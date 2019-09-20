// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Reflection;

namespace Thunder.BuildSystem.Sdk.Helpers
{
    internal static class ThunderBuildVersion
    {
        private static string _version;

        public static string Current
        {
            get
            {
                if (_version == null)
                {
                    var assembly = typeof(ThunderBuildVersion).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
                    if (assembly != null)
                    {
                        _version = assembly.InformationalVersion;
                    }
                }

                return _version;
            }
        }
    }
}
