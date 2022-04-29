using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

using Microsoft.ML.Data;

using MLIDS.lib.Common;

using MongoDB.Bson.Serialization.Attributes;

using PacketDotNet;

[assembly: InternalsVisibleTo("MLIDS.UnitTests")]
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
        public bool Label { get; internal set; }

        [LoadColumn(1)]
        [NoColumn]
        public string ProtocolType { get; internal set; }

        [LoadColumn(2)]
        [NoColumn]
        public string SourceIPAddress { get; internal set; }

        [LoadColumn(3)]
        public float SourcePort { get; internal set; }

        [LoadColumn(4)]
        [NoColumn]
        public string DestinationIPAddress { get; internal set; }

        [LoadColumn(5)]
        public float DestinationPort { get; internal set; }

        [LoadColumn(6)]
        public float HeaderSize { get; internal set; }

        [LoadColumn(7)]
        public float PayloadSize { get; internal set; }

        [LoadColumn(8)]
        [NoColumn]
        public string PacketContent { get; internal set; }

        [NoColumn]
        public string HostName { get; internal set; }

        [NoColumn]
        public int Version { get; internal set; }

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        [NoColumn] public bool IsClean { get; internal set; }

        [NoColumn] public DateTime Timestamp { get; internal set; }

        [LoadColumn(9)]
        public bool IsEncrypted { get; internal set; }

        [NoColumn] public string DecodedPayload { get; internal set; }

        private const string UNICODE_START_OF_HEADING = "\u0001";

        /// <summary>
        /// Used for Unit Tests Only and Deserialization
        /// </summary>
        public PayloadItem()
        {

        }

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

                StringBuilder decodedSb = new StringBuilder();

                foreach (string hex in hexValuesSplit)
                {
                    decodedSb.Append((char)Convert.ToInt32(hex, 16));
                }

                DecodedPayload = decodedSb.ToString().Replace(Convert.ToChar(0x0).ToString(), "");

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