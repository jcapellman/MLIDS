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
    }
}