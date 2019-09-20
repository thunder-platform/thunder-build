using System;
using System.Collections.Generic;

namespace Thunder.BuildSystem.Sdk.Internal.ProjectModel
{
    internal class SolutionInfo
    {
        public SolutionInfo(string fullPath, IReadOnlyList<string> projects)
        {
            if (string.IsNullOrEmpty(fullPath))
            {
                throw new ArgumentException(nameof(fullPath));
            }

            FullPath = fullPath;
            Projects = projects ?? throw new ArgumentNullException(nameof(projects));
        }

        public string FullPath { get; }

        public IReadOnlyList<string> Projects { get; }
    }
}
