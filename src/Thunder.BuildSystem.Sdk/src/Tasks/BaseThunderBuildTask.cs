using System;
using System.Collections.Concurrent;
using System.IO;
using Microsoft.Build.Utilities;

namespace Thunder.BuildSystem.Sdk.Tasks
{
    public abstract class BaseThunderBuildTask : Task
    {
        public bool ShowStackTrace { get; set; }

        protected ConcurrentQueue<Exception> Exceptions { get; } = new ConcurrentQueue<Exception>();

        public override bool Execute()
        {
            try
            {
                ExecuteThunderBuildTask();
            }
            catch (Exception exception)
            {
                Exceptions.Enqueue(exception);
            }

            foreach (Exception exception in Exceptions)
            {
                Log.LogError(GetFullMessageFromException(exception));
            }

            if (ShowStackTrace)
            {
                foreach (Exception exception in Exceptions)
                {
                    Log.LogErrorFromException(exception, true, true, null);
                }
            }

            return !Log.HasLoggedErrors;
        }

        protected abstract void ExecuteThunderBuildTask();

        protected void WriteFile(string content, string destination)
        {
            var file = new FileInfo(destination);
            file.Directory?.Create();
            File.WriteAllText(file.FullName, content);
        }

        private string GetFullMessageFromException(Exception exception)
        {
            return exception.InnerException == null
                ? exception.Message
                : exception.Message + " --> " + GetFullMessageFromException(exception.InnerException);
        }
    }
}
