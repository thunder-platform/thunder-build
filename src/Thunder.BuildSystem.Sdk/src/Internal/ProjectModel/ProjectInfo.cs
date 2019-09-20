using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Thunder.BuildSystem.Sdk.Internal.ProjectModel
{
    internal class ProjectInfo
    {
        public ProjectInfo(
            string fullPath,
            string projectExtensionsPath,
            IReadOnlyList<ProjectFrameworkInfo> frameworks)
        {
            if (!Path.IsPathRooted(fullPath))
            {
                throw new ArgumentException("Path must be absolute", nameof(fullPath));
            }

            Frameworks = frameworks ?? throw new ArgumentNullException(nameof(frameworks));

            FullPath = fullPath;
            FileName = Path.GetFileName(fullPath);
            Directory = Path.GetDirectoryName(FullPath);
            ProjectExtensionsPath = projectExtensionsPath ?? Path.Combine(Directory, "obj");

            foreach (var dep in frameworks.SelectMany(f => f.Dependencies))
            {
                dep.Value.Project = this;
            }
        }

        public string FullPath { get; }

        public string FileName { get; }

        public string ProjectExtensionsPath { get; }

        public string Directory { get; }

        public IReadOnlyList<ProjectFrameworkInfo> Frameworks { get; }
    }
}
