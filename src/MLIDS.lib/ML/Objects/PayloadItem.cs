using System;

using Microsoft.ML.Data;

using PacketDotNet;

namespace MLIDS.lib.ML.Objects
{
    public class PayloadItem
    {
        [LoadColumn(0)]
        public string ProtocolType { get; private set; }

        [LoadColumn(1)]
        public string SourceIPAddress { get; private set; }

        [LoadColumn(2)]
        public int SourcePort { get; private set; }

        [LoadColumn(3)]
        public string DestinationIPAddress { get; private set; }

        [LoadColumn(4)]
        public int DestinationPort { get; private set; }

        [LoadColumn(5)]
        public int HeaderSize { get; private set; }

        [LoadColumn(6)]
        public int PayloadSize { get; private set; }

        [LoadColumn(7)]
        public string PacketContent { get; private set; }

        public PayloadItem(string protocolType, IPPacket sourcePacket, TransportPacket payloadPacket)
        {
            ProtocolType = protocolType;

            SourceIPAddress = sourcePacket.SourceAddress.ToString();
            SourcePort = payloadPacket.SourcePort;

            DestinationIPAddress = sourcePacket.DestinationAddress.ToString();
            DestinationPort = payloadPacket.DestinationPort;

            HeaderSize = payloadPacket.HeaderData.Length;
            PayloadSize = payloadPacket.PayloadData.Length;

            PacketContent = BitConverter.ToString(payloadPacket.PayloadData);
        }
    }
}