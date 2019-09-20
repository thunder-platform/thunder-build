// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Build.Utilities;

namespace Thunder.BuildSystem.Sdk.Internal
{
    public static class LoggingExtensions
    {
        public static void LogThunderBuildError(this TaskLoggingHelper logger, int code, string message, params object[] messageArgs)
            => LogThunderBuildError(logger, null, 0, code, message, messageArgs: messageArgs);

        public static void LogThunderBuildError(this TaskLoggingHelper logger, string filename, int code, string message, params object[] messageArgs)
            => LogThunderBuildError(logger, null, 0, code, message, messageArgs: messageArgs);

        public static void LogThunderBuildError(this TaskLoggingHelper logger, string filename, int lineNumber, int code, string message, params object[] messageArgs)
        {
            logger.LogError(null, ThunderBuildErrors.Prefix + code, null, filename, lineNumber, 0, 0, 0, message, messageArgs: messageArgs);
        }

        public static void LogThunderBuildWarning(this TaskLoggingHelper logger, int code, string message, params object[] messageArgs)
            => LogThunderBuildWarning(logger, null, code, message, messageArgs: messageArgs);

        public static void LogThunderBuildWarning(this TaskLoggingHelper logger, string filename, int code, string message, params object[] messageArgs)
        {
            logger.LogWarning(null, ThunderBuildErrors.Prefix + code, null, filename, 0, 0, 0, 0, message, messageArgs: messageArgs);
        }
    }
}
