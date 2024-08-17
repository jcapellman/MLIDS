namespace MLIDS.lib.Common
{
    public static class Constants
    {
        public const string DAL_HostIP = "127.0.0.1";

        public const int DAL_HostPort = 27017;

        public const string DAL_FileName = "litedb.db";

        public const int API_VERSION = 2;

        public const string SETTINGS_FILENAME = "settings.json";

        public const int ML_SEED = 2020;

        public const int PACKET_READ_TIMEOUT_MS = 1000;

        public const string MESSAGE_NPCAP_NOT_FOUND = "NPCAP Driver is required and is not installed - please install (https://nmap.org/npcap/)";

        public const string MESSAGE_SAVE_SETTINGS = "Settings were saved successfully";
    }
}