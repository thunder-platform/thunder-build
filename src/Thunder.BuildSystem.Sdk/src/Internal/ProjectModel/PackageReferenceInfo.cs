using System;
using System.Collections.Generic;

namespace Thunder.BuildSystem.Sdk.Internal.ProjectModel
{
    internal class PackageReferenceInfo
    {
        public PackageReferenceInfo(string id, string version, bool isImplicitlyDefined, IReadOnlyList<string> noWarn)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(nameof(id));
            }

            Id = id;
            Version = version;
            IsImplicitlyDefined = isImplicitlyDefined;
            NoWarn = noWarn;
        }

        public string Id { get; }

        public string Version { get; }

        public bool IsImplicitlyDefined { get; }

        public IReadOnlyList<string> NoWarn { get; }

        public ProjectInfo Project { get; internal set; }
    }
}
