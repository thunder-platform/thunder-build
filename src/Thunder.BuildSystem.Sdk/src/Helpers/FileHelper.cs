using System.IO;

namespace Thunder.BuildSystem.Sdk.Helpers
{
    internal class FileHelper
    {
        public static string EnsureTrailingSlash(string path)
            => !HasTrailingSlash(path)
                ? path + Path.DirectorySeparatorChar
                : path;

        public static bool HasTrailingSlash(string path)
            => !string.IsNullOrEmpty(path) && (path[path.Length - 1] == Path.DirectorySeparatorChar ||
                                               path[path.Length - 1] == Path.AltDirectorySeparatorChar);

        public static string NormalizePath(string path)
            => string.IsNullOrEmpty(path)
                ? path
                : path.Replace('\\', '/');
    }
}
