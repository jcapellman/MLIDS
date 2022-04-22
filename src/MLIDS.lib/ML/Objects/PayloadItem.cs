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

        [NoColumn] public DateTime Timestamp { get; private set; }

        [LoadColumn(9)]
        public bool IsEncrypted { get; private set; }

        [NoColumn] public string DecodedPayload { get; private set; }

        private const string UNICODE_START_OF_HEADING = "\u0001";

        public PayloadItem(ProtocolType protocolType, IPPacket sourcePacket, bool clean)
        {
            if (sourcePacket == null)
            {
                Log.Error($"PayloadItem - sourcePacket was null");

                throw new ArgumentNullException(nameof(sourcePacket));
            }

            Label = clean;

            IsClean = clean;

            ProtocolType = protocolType.ToString();

            SourceIPAddress = sourcePacket.SourceAddress.ToString();
            DestinationIPAddress = sourcePacket.DestinationAddress.ToString();

            HostName = Environment.MachineName;

            Version = Constants.API_VERSION;

            Timestamp = DateTime.Now;

            DecodedPayload = string.Empty;
        }

        public PayloadItem(ProtocolType protocolType, IPPacket sourcePacket, TransportPacket payloadPacket, bool clean) : this(protocolType, sourcePacket, clean)
        {
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

            if (!string.IsNullOrEmpty(PacketContent)) { 
                string[] hexValuesSplit = PacketContent.Split('-');

                foreach (string hex in hexValuesSplit)
                {
                    DecodedPayload += (char)Convert.ToInt32(hex, 16);
                }

                DecodedPayload = DecodedPayload.Replace(Convert.ToChar(0x0).ToString(), "");

                IsEncrypted = (System.Text.Encoding.UTF8.GetByteCount(DecodedPayload) != DecodedPayload.Length);

                if (DecodedPayload == UNICODE_START_OF_HEADING)
                {
                    DecodedPayload = string.Empty;
                }
            }
        }

        public PayloadItem(ProtocolType protocolType, IPPacket sourcePacket, InternetPacket internetPacket, bool clean) : this(protocolType, sourcePacket, clean)
        {
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

        public override string ToString() => $"({ProtocolType}): {SourceIPAddress}:{SourcePort} to " +
                                             $"{DestinationIPAddress}:{DestinationPort} - Header Size: {HeaderSize} | Packet Size: {PayloadSize} | Encrypted: {IsEncrypted} | Decoded Payload: {(DecodedPayload == string.Empty ? "(Empty Payload)" : DecodedPayload)}";
    }
}