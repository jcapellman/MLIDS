using System;
using System.Net.Sockets;

namespace jcIDS.lib.Helpers
{
    public static class ExtensionMethods
    {
        public static ProtocolType ToProtocolType(this byte value)
        {
            if (Enum.IsDefined(typeof(ProtocolType), (int)value))
            {
                return (ProtocolType)value;
            }

            return ProtocolType.Unknown;
        }
    }
}