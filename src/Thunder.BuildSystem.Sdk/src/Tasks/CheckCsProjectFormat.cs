using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;

namespace Thunder.BuildSystem.Sdk.Tasks
{
    public class CheckCsProjectFormat : BaseThunderBuildTask
    {
        [Required]
        public ITaskItem[] CsProjectFiles { get; set; }

        protected override void ExecuteThunderBuildTask()
        {
            var errors = new List<string>();
            foreach (var csProjectFile in CsProjectFiles)
            {
                if (!EnsureNoProjectReference(csProjectFile))
                {
                    errors.Add($"There is a ProjectReference usage in {csProjectFile}. This should be fixed as Reference instead.");
                }
            }

            if (errors.Count > 0)
            {
                Log.LogError(string.Join("\r\n", errors));
            }
        }

        private bool EnsureNoProjectReference(ITaskItem csProjectFile)
        {
            var content = File.ReadAllText(csProjectFile.ItemSpec);
            var matches = Regex.Matches(content, "(ProjectReference)w*", RegexOptions.Multiline);
            if (matches.Count != 0)
            {
                return false;
            }

            return true;
        }
    }
}
