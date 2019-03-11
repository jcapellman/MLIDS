using System.Net.Sockets;

using jcIDS.lib.CommonObjects;

using jcIDS.web.DAL.Tables.Base;

namespace jcIDS.web.DAL.Tables
{
    public class Packets : BaseTable
    {
        public string SourceIP { get; set; }

        public string DestinationIP { get; set; }

        public ProtocolType Protocol { get; set; }

        public int DeviceID { get; set; }

        public Packets(Packet packet)
        {
            SourceIP = packet.OriginationAddress;
        }
    }
}