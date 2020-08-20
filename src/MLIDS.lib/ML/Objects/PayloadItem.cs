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
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        [BsonId]
        [DataMember]
        [NoColumn]
        public Guid Guid { get; set; }

        [LoadColumn(0)]
        public bool Label { get; private set; }

        [LoadColumn(1)]
        [NoColumn]
        public string ProtocolType { get; private set; }

        [LoadColumn(2)]
        [NoColumn]
        public string SourceIPAddress { get; private set; }

        [LoadColumn(3)]
        public float SourcePort { get; private set; }

        [LoadColumn(4)]
        [NoColumn]
        public string DestinationIPAddress { get; private set; }

        [LoadColumn(5)]
        public float DestinationPort { get; private set; }

        [LoadColumn(6)]
        public float HeaderSize { get; private set; }

        [LoadColumn(7)]
        public float PayloadSize { get; private set; }

        [LoadColumn(8)]
        [NoColumn]
        public string PacketContent { get; private set; }

        [NoColumn]
        public string HostName { get; private set; }

        [NoColumn]
        public int Version { get; private set; }

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        [NoColumn] public bool IsClean { get; private set; }

        public PayloadItem(string protocolType, IPPacket sourcePacket, bool clean)
        {
            if (sourcePacket == null)
            {
                Log.Error($"PayloadItem - sourcePacket was null");

                throw new ArgumentNullException(nameof(sourcePacket));
            }

            Label = clean;

            IsClean = clean;

            ProtocolType = protocolType;

            SourceIPAddress = sourcePacket.SourceAddress.ToString();
            DestinationIPAddress = sourcePacket.DestinationAddress.ToString();

            HostName = Environment.MachineName;

            Version = Constants.API_VERSION;
        }

        public PayloadItem(string protocolType, IPPacket sourcePacket, TransportPacket payloadPacket, bool clean) : this(protocolType, sourcePacket, clean)
        {
            if (string.IsNullOrEmpty(protocolType))
            {
                Log.Error($"PayloadItem - protocolType was null or empty");

                throw new ArgumentNullException(nameof(protocolType));
            }

            if (payloadPacket == null)
            {
                Log.Error($"PayloadItem - payloadPacket was null");

                throw new ArgumentNullException(nameof(payloadPacket));
            }

            SourceIPAddress = sourcePacket.SourceAddress.ToString();
            SourcePort = payloadPacket.SourcePort;
           
            DestinationPort = payloadPacket.DestinationPort;

            HeaderSize = payloadPacket.HeaderData.Length;
            PayloadSize = payloadPacket.PayloadData.Length;

            PacketContent = BitConverter.ToString(payloadPacket.PayloadData);
        }

        public PayloadItem(string protocolType, IPPacket sourcePacket, InternetPacket internetPacket, bool clean)
        {
            if (string.IsNullOrEmpty(protocolType))
            {
                Log.Error($"PayloadItem - protocolType was null or empty");

                throw new ArgumentNullException(nameof(protocolType));
            }

            if (sourcePacket == null)
            {
                Log.Error($"PayloadItem - sourcePacket was null");

                throw new ArgumentNullException(nameof(sourcePacket));
            }

            if (internetPacket == null)
            {
                Log.Error($"PayloadItem - payloadPacket was null");

                throw new ArgumentNullException(nameof(internetPacket));
            }

            DestinationIPAddress = sourcePacket.DestinationAddress.ToString();
            
            HeaderSize = internetPacket.HeaderData.Length;

            if (internetPacket.PayloadData == null)
            {
                return;
            }

            PayloadSize = internetPacket.PayloadData.Length;

            PacketContent = BitConverter.ToString(internetPacket.PayloadData);
        }

        public override string ToString() => $"{SourceIPAddress}:{SourcePort} to {DestinationIPAddress}:{DestinationPort} of size {PayloadSize}";
    }
}