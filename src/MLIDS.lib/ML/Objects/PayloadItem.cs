using System;
using System.Runtime.Serialization;

using Microsoft.ML.Data;

using MLIDS.lib.Common;

using MongoDB.Bson.Serialization.Attributes;

using PacketDotNet;

namespace MLIDS.lib.ML.Objects
{
    public class PayloadItem
    {
        [BsonId]
        [DataMember]
        [NoColumn]
        public Guid Guid { get; set; }

        [LoadColumn(0)]
        public bool Label { get; private set; }

        [LoadColumn(1)]
        public string ProtocolType { get; private set; }

        [LoadColumn(2)]
        public string SourceIPAddress { get; private set; }

        [LoadColumn(3)]
        public int SourcePort { get; private set; }

        [LoadColumn(4)]
        public string DestinationIPAddress { get; private set; }

        [LoadColumn(5)]
        public int DestinationPort { get; private set; }

        [LoadColumn(6)]
        public int HeaderSize { get; private set; }

        [LoadColumn(7)]
        public int PayloadSize { get; private set; }

        [LoadColumn(8)]
        public string PacketContent { get; private set; }

        [NoColumn]
        public string HostName { get; private set; }

        [NoColumn]
        public int Version { get; private set; }

        public PayloadItem(string protocolType, IPPacket sourcePacket, TransportPacket payloadPacket, bool clean)
        {
            Label = clean;

            ProtocolType = protocolType;

            SourceIPAddress = sourcePacket.SourceAddress.ToString();
            SourcePort = payloadPacket.SourcePort;

            DestinationIPAddress = sourcePacket.DestinationAddress.ToString();
            DestinationPort = payloadPacket.DestinationPort;

            HeaderSize = payloadPacket.HeaderData.Length;
            PayloadSize = payloadPacket.PayloadData.Length;

            PacketContent = BitConverter.ToString(payloadPacket.PayloadData);

            HostName = Environment.MachineName;

            Version = Constants.API_VERSION;
        }
    }
}