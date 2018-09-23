using System.Collections.Generic;
using System.Net.NetworkInformation;
using jcIDS.library.core.DAL.Objects;

namespace jcIDS.library.core.PlatformInterfaces
{
    public interface INetworkInterfaces
    {
        bool IsOnline();

        List<NetworkDeviceObject> ScanDevices();

        string DeviceIPAddress { get; }

        NetworkInterface GetNetworkInterface();
    }
}