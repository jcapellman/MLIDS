using System.IO;

using jcIDS.app.Common;

using Newtonsoft.Json;

namespace jcIDS.app.Managers
{
    public class SettingsManager
    {
        public static Settings LoadSettings(string fileName = Constants.DEFAULT_SETTINGS_FILE) => 
            !File.Exists(fileName) ? null : JsonConvert.DeserializeObject<Settings>(File.ReadAllText(fileName));
    }
}