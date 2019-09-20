using System.Diagnostics;
using System.Text;

namespace Thunder.BuildSystem.Sdk.Helpers
{
    internal class ProcessHelper
    {
        public static string ExecuteProcess(ProcessStartInfo processStartInfo, int timeOutInMilliseconds)
        {
            var outputDataBuilder = new StringBuilder();

            using (var process = new Process())
            {
                process.StartInfo = processStartInfo;

                process.ErrorDataReceived += (sender, message) =>
                {
                    if (!string.IsNullOrEmpty(message.Data))
                    {
                        outputDataBuilder.AppendLine(message.Data);
                    }
                };

                process.OutputDataReceived += (sender, message) =>
                {
                    if (!string.IsNullOrEmpty(message.Data))
                    {
                        outputDataBuilder.AppendLine(message.Data);
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit(timeOutInMilliseconds);
            }

            return outputDataBuilder.ToString();
        }
    }
}
