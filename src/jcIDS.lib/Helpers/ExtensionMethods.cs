using System.Net.Sockets;

namespace jcIDS.lib.Helpers
{
    public static class ExtensionMethods
    {
        public static ProtocolType ToProtocolType(this byte value)
        {
            switch (value)
            {
                case 1:
                    return ProtocolType.Icmp;
                case 2:
                    return ProtocolType.Igmp;
                case 6:
                    return ProtocolType.Tcp;
                case 17:
                    return ProtocolType.Udp;
                default:
                    return ProtocolType.Unknown;
            }
        }
    }
}