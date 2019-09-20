using System.IO;
using Newtonsoft.Json;

namespace Thunder.BuildSystem.Sdk
{
    public class ThunderBuildSettings
    {
        [JsonProperty("common")]
        public CommonSetting CommonSettings { get; set; }

        public static ThunderBuildSettings Load(string filePath)
        {
            using (var file = File.OpenText(filePath))
            {
                using (var json = new JsonTextReader(file))
                {
                    var serializer = new JsonSerializer();
                    return serializer.Deserialize<ThunderBuildSettings>(json);
                }
            }
        }

        public class CommonSetting
        {
            public string Project { get; set; }
        }
    }
}
