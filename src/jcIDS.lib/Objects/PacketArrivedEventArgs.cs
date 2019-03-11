using System;
using System.Net.Sockets;

using jcIDS.lib.Common;

namespace jcIDS.lib.Objects
{
    public class PacketArrivedEventArgs : EventArgs
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

        public PacketArrivedEventArgs()
        {
            Protocol = ProtocolType.Unspecified;

            DestinationPort = "";
            OriginationPort = "";
            DestinationAddress = "";
            OriginationAddress = "";
            IPVersion = "";

            ReceiveBuffer = new byte[Constants.RECEIVE_BUFFER_LENGTH];
            IPHeaderBuffer = new byte[Constants.RECEIVE_BUFFER_LENGTH];
            MessageBuffer = new byte[Constants.RECEIVE_BUFFER_LENGTH];
        }

        public override string ToString() => 
            $"{OriginationAddress}:{OriginationPort} ({Protocol}) - {DestinationAddress}:{DestinationPort} - {PacketLength}";
    }
}