using System;
using System.Collections.Generic;
using System.Reflection;
using PacketDotNet;

namespace MLIDS.lib.Objects
{
    public class PayloadItem
    {
        public string ProtocolType { get; private set; }

        public string SourceIPAddress { get; private set; }

        public int SourcePort { get; private set; }

        public string DestinationIPAddress { get; private set; }

        public int DestinationPort { get; private set; }

        public int HeaderSize { get; private set; }

        public int PayloadSize { get; private set; }

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

        public override string ToString()
        {
            var members = typeof(PayloadItem).GetMembers(BindingFlags.Public);

            var properties = new List<string>();

            foreach (var member in members)
            {
                if (member is FieldInfo fieldInfo)
                {
                    properties.Add(fieldInfo.GetValue(this).ToString());
                }
            }

            return string.Join(',', properties);
        }
    }
}