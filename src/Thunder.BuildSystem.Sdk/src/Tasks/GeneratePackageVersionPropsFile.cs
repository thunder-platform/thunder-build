using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Thunder.BuildSystem.Sdk.Helpers;

namespace Thunder.BuildSystem.Sdk.Tasks
{
    public class GeneratePackageVersionPropsFile : Task
    {
        [Required]
        public ITaskItem[] Packages { get; set; }

        [Required]
        public string OutputPath { get; set; }

        public bool AddOverrideImport { get; set; }

        public string[] AdditionalImports { get; set; }

        public bool SuppressVariableLabels { get; set; }

        public override bool Execute()
        {
            OutputPath = OutputPath.Replace('\\', '/');
            Directory.CreateDirectory(Path.GetDirectoryName(OutputPath));

            DependencyVersionsFile depsFile;
            if (File.Exists(OutputPath))
            {
                if (!DependencyVersionsFile.TryLoad(OutputPath, out depsFile))
                {
                    depsFile = DependencyVersionsFile.Create(AddOverrideImport, AdditionalImports);
                    Log.LogWarning($"Could not load the existing deps file from {OutputPath}. This file will be overwritten.");
                }
            }
            else
            {
                depsFile = DependencyVersionsFile.Create(AddOverrideImport, AdditionalImports);
            }

            var varNames = new HashSet<string>();
            foreach (var pkg in Packages)
            {
                var packageVersion = pkg.GetMetadata("Version");

                if (string.IsNullOrEmpty(packageVersion))
                {
                    Log.LogError("Package {0} is missing the Version metadata", pkg.ItemSpec);
                    continue;
                }

                string packageVarName;
                if (!string.IsNullOrEmpty(pkg.GetMetadata("VariableName")))
                {
                    packageVarName = pkg.GetMetadata("VariableName");
                    if (!packageVarName.EndsWith("Version", StringComparison.Ordinal))
                    {
                        Log.LogError("VariableName for {0} must end in 'Version'", pkg.ItemSpec);
                        continue;
                    }
                }
                else
                {
                    packageVarName = DependencyVersionsFile.GetVariableName(pkg.ItemSpec);
                }

                if (varNames.Contains(packageVarName))
                {
                    Log.LogError("Multiple packages would produce {0} in the generated dependencies.props file. Set VariableName to differentiate the packages manually", packageVarName);
                    continue;
                }

                var item = depsFile.Update(packageVarName, packageVersion);
                if (!SuppressVariableLabels)
                {
                    item.SetLabel(pkg.ItemSpec);
                }
            }

            depsFile.Save(OutputPath);
            Log.LogMessage(MessageImportance.Normal, $"Generated {OutputPath}");
            return !Log.HasLoggedErrors;
        }
    }
}
