﻿using System;
using System.IO;
using System.Text.Json;

using MLIDS.lib.Common;

namespace MLIDS.lib.Containers
{
    public class SettingsItem
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        public string DAL_HostIP { get; set; }

        public int DAL_HostPort { get; set; }

        public SettingsItem()
        {
            DAL_HostIP = Constants.DAL_HostIP;

            DAL_HostPort = Constants.DAL_HostPort;
        }

        public static SettingsItem Load(string fileName = Constants.SETTINGS_FILENAME)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                Log.Error("SettingsItem::Load - Filename was null or empty");

                throw new ArgumentNullException(nameof(fileName));
            }

            var fullPath = Path.Combine(AppContext.BaseDirectory, fileName);

            if (!File.Exists(fullPath))
            {
                Log.Warn($"SettingsItem::Load - Filename ({fileName}) does not exist, creating with defaults");

                var settingsItem = new SettingsItem();

                File.WriteAllText(fullPath, JsonSerializer.Serialize(settingsItem));

                return settingsItem;
            }

            Log.Debug($"SettingsItem::Load - File ({fileName}) was loaded");

            return JsonSerializer.Deserialize<SettingsItem>(File.ReadAllBytes(fullPath));
        }
    }
}