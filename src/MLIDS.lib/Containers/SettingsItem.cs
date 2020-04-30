using System;
using System.IO;
using System.Text.Json;

using MLIDS.lib.Common;

namespace MLIDS.lib.Containers
{
    public class SettingsItem
    {
        public string DAL_HostIP { get; set; }

        public int DAL_HostPort { get; set; }

        public SettingsItem()
        {
            DAL_HostIP = Constants.DAL_HostIP;

            DAL_HostPort = Constants.DAL_HostPort;
        }

        public static SettingsItem Load(string fileName = Constants.SETTINGS_FILENAME)
        {
            var fullPath = Path.Combine(AppContext.BaseDirectory, fileName);

            if (!File.Exists(fullPath))
            {
                var settingsItem = new SettingsItem();

                File.WriteAllText(fullPath, JsonSerializer.Serialize(settingsItem));

                return settingsItem;
            }

            return JsonSerializer.Deserialize<SettingsItem>(File.ReadAllBytes(fullPath));
        }
    }
}