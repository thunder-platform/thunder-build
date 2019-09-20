using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Thunder.BuildSystem.Sdk.Tasks
{
    public class GetCommonSettings : Task
    {
        /// <summary>
        /// The path to the thunder.json file.
        /// </summary>
        [Required]
        public string ConfigFile { get; set; }

        [Output]
        public string Project { get; set; }

        public override bool Execute()
        {
            if (!File.Exists(ConfigFile))
            {
                Log.LogError($"Could not load the ThunderBuild config file from '{ConfigFile}'");
                return false;
            }

            var settings = ThunderBuildSettings.Load(ConfigFile);

            if (settings?.CommonSettings == null)
            {
                Log.LogMessage(MessageImportance.Normal, "No recognized common settings specified.");
                return true;
            }

            var commonSettings = settings.CommonSettings;
            Project = commonSettings.Project;

            return !Log.HasLoggedErrors;
        }
    }
}
