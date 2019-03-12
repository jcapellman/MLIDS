using System.Net.Sockets;

using jcIDS.lib.CommonObjects;

using jcIDS.web.DAL.Tables.Base;

namespace jcIDS.web.DAL.Tables
{
    public class Packets : BaseTable
    {
        public ProtocolType Protocol;

        public string DestinationPort;

        public string OriginationPort;

        public string DestinationAddress;

        public string OriginationAddress;

        public string IPVersion;

        public uint PacketLength;

        public uint MessageLength;

        public uint HeaderLength;

        public int DeviceID { get; set; }

        public Packets(Packet packet, int deviceID)
        {
            Protocol = packet.Protocol;

            OriginationAddress = packet.OriginationAddress;
            OriginationPort = packet.OriginationPort;

            DestinationAddress = packet.DestinationAddress;
            DestinationPort = packet.DestinationPort;

            IPVersion = packet.IPVersion;

            PacketLength = packet.PacketLength;
            MessageLength = packet.MessageLength;
            HeaderLength = packet.HeaderLength;

            DeviceID = deviceID;
        }
    }
}