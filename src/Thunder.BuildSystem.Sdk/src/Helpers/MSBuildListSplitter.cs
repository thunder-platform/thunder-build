using System;
using System.Collections.Generic;

namespace Thunder.BuildSystem.Sdk.Helpers
{
    internal static class MSBuildListSplitter
    {
        public static IDictionary<string, string> GetNamedProperties(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }

            return GetNamedProperties(input.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public static IDictionary<string, string> GetNamedProperties(string[] input)
        {
            var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (input == null)
            {
                return values;
            }

            foreach (var item in input)
            {
                var splitIdx = item.IndexOf('=');
                if (splitIdx < 0)
                {
                    continue;
                }

                var key = item.Substring(0, splitIdx).Trim();
                if (string.IsNullOrEmpty(key))
                {
                    continue;
                }

                var value = item.Substring(splitIdx + 1);
                values[key] = value;
            }

            return values;
        }
    }
}
