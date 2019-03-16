using System.Net.Sockets;

using Newtonsoft.Json;

namespace jcIDS.lib.CommonObjects
{
    public class Packet
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

        public byte[] ReceiveBuffer;

        public byte[] IPHeaderBuffer;

        public byte[] MessageBuffer;

        public Packet()
        {
            Protocol = ProtocolType.Unspecified;

            DestinationPort = "";
            OriginationPort = "";
            DestinationAddress = "";
            OriginationAddress = "";
            IPVersion = "";
        }

        public override string ToString() =>
            $"{OriginationAddress}:{OriginationPort} ({Protocol}) - {DestinationAddress}:{DestinationPort} - {PacketLength}";

        public string ToJSON() => JsonConvert.SerializeObject(this);
    }
}