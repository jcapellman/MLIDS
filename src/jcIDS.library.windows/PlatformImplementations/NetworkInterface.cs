using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using jcIDS.library.core.PlatformInterfaces;

namespace jcIDS.library.windows.PlatformImplementations
{
    public class NetworkInterface : INetworkInterfaces
    {
        public bool IsOnline()
        {
            throw new System.NotImplementedException();
        }

        public string[] ScanDevices()
        {
            var devices = new ConcurrentBag<string>();
            
            var data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var buffer = Encoding.ASCII.GetBytes(data);
            var timeout = 120;

            Parallel.For(1, 255, (x, state) =>
            {
                var pingSender = new Ping();

                var options = new PingOptions { DontFragment = true };

                var ipAddress = $"192.168.2.{x}";

                var reply = pingSender.Send(ipAddress, timeout, buffer, options);

                if (reply == null || reply.Status != IPStatus.Success)
                {
                    return;
                }

                devices.Add(ipAddress);
            });

            return devices.ToArray();
        }
    }
}