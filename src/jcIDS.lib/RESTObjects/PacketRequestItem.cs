using System.Collections.Generic;

using jcIDS.lib.CommonObjects;
using jcIDS.lib.RESTObjects.Base;

namespace jcIDS.lib.RESTObjects
{
    public class PacketRequestItem : BaseRequestItem
    {
        public List<Packet> Packets { get; set; }
    }
}