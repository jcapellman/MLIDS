using System;

using jcIDS.library.core.DAL.Objects.Base;

namespace jcIDS.library.core.DAL.Objects
{
    public class NetworkDeviceObject : BaseObject
    {
        public string IPV4Address { get; set; }

        public string IPV6Address { get; set; }

        public string MAC { get; set; }

        public DateTime LastOnline { get; set; }
    }
}