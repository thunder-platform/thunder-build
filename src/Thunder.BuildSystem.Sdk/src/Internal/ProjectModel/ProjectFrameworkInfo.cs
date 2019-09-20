using System;
using System.Collections.Generic;
using NuGet.Frameworks;

namespace Thunder.BuildSystem.Sdk.Internal.ProjectModel
{
    internal class ProjectFrameworkInfo
    {
        public ProjectFrameworkInfo(NuGetFramework targetFramework, IReadOnlyDictionary<string, PackageReferenceInfo> dependencies)
        {
            TargetFramework = targetFramework ?? throw new ArgumentNullException(nameof(targetFramework));
            Dependencies = dependencies ?? throw new ArgumentNullException(nameof(dependencies));
        }

        public NuGetFramework TargetFramework { get; }

        public IReadOnlyDictionary<string, PackageReferenceInfo> Dependencies { get; }
    }
}
