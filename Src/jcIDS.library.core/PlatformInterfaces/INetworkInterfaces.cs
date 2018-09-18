using System.Collections.Generic;

using jcIDS.library.core.DAL.Objects;

namespace jcIDS.library.core.PlatformInterfaces
{
    public interface INetworkInterfaces
    {
        bool IsOnline();

        List<NetworkDeviceObject> ScanDevices();
    }
}