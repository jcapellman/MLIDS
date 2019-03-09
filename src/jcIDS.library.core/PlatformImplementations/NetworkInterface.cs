using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using jcIDS.library.core.DAL.Objects;
using jcIDS.library.core.PlatformInterfaces;
using NLog;

namespace jcIDS.library.core.PlatformImplementations
{
    public class NetworkInterface : INetworkInterfaces
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        public bool IsOnline() => GetNetworkInterface().OperationalStatus == OperationalStatus.Up;

        public List<NetworkDeviceObject> ScanDevices()
        {
            var currentNetworkInterface = GetNetworkInterface();

            if (currentNetworkInterface == null)
            {
                _log.Warn("No Network Inteface could be found, ending Network Device Enumeration");

                return new List<NetworkDeviceObject>();
            }

            _log.Info($"Using {currentNetworkInterface.Description} for network connection...");

            var devices = new ConcurrentBag<NetworkDeviceObject>();

            var buffer = Encoding.ASCII.GetBytes("A".PadRight(40, 'A'));
            
            var timeout = 120;

            var deviceIP = DeviceIPAddress;

            if (string.IsNullOrEmpty(deviceIP))
            {
                return new List<NetworkDeviceObject>();
            }
            
            var baseDeviceIP = string.Join(".", deviceIP.Split('.').Take(3));

            Parallel.For(1, 255, (x, state) =>
            {
                var pingSender = new Ping();

                var options = new PingOptions { DontFragment = true };

                var ipAddress = $"{baseDeviceIP}.{x}";

                var reply = pingSender.Send(ipAddress, timeout, buffer, options);
                
                if (reply == null || reply.Status != IPStatus.Success)
                {
                    return;
                }

                var device = new NetworkDeviceObject()
                {
                    IPV4Address = ipAddress,
                    LastOnline = DateTime.Now
                };

                try
                {
                    var hostEntry = Dns.GetHostEntry(ipAddress);

                    device.ResourceName = hostEntry.HostName;
                } catch (Exception) { }
                
                devices.Add(device);
            });

            return devices.ToList();
        }

        public string DeviceIPAddress
        {
            get
            {
                var ipAddress = GetNetworkInterface()?.GetIPProperties().UnicastAddresses
                    .FirstOrDefault(a => a.Address.AddressFamily == AddressFamily.InterNetwork);

                return ipAddress == null ? string.Empty : ipAddress.Address.ToString();
            }
        }

        public System.Net.NetworkInformation.NetworkInterface GetNetworkInterface()
        {
            var networkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();

            return networkInterfaces.FirstOrDefault(a =>
                a.OperationalStatus == OperationalStatus.Up &&
                (a.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                 a.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet));
        }
    }
}