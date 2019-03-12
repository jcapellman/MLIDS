using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

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

        public static string SHA1(this string value)
        {
            using (var sha1 = new SHA1Managed())
            {
                return BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(value))).Replace("-", "");
            }
        }
    }
}