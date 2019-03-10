using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

using jcIDS.lib.Common;
using jcIDS.lib.Objects;

namespace jcIDS.lib.Managers
{
    public class SocketListener : IDisposable
    {
        readonly byte[] _receiveBufBytes;
        private Socket _socket;
        
        public event PacketArrivedEventHandler PacketArrival;
        public delegate void PacketArrivedEventHandler(object sender, PacketArrivedEventArgs args);

        protected virtual void OnPacketArrival(PacketArrivedEventArgs e)
        {
            PacketArrival?.Invoke(this, e);
        }

        /// <summary>
        /// Raw Socket Constructor
        /// </summary>
        public SocketListener()
        {
            _receiveBufBytes = new byte[Constants.len_receive_buf];

            Initialize();
        }

        private static NetworkInterface GetNetworkInterface() {
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            return networkInterfaces.FirstOrDefault(a =>
                a.OperationalStatus == OperationalStatus.Up &&
                (a.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                 a.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet));
        }

        private void Initialize()
        {
            var networkInterface = GetNetworkInterface();

            var ipAddress = networkInterface?.GetIPProperties().UnicastAddresses
                .FirstOrDefault(a => a.Address.AddressFamily == AddressFamily.InterNetwork);

            CreateAndBindSocket(ipAddress?.Address.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="protocol"></param>
        private void CreateAndBindSocket(string ip, int port = 0, ProtocolType protocol = ProtocolType.IP)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, protocol)
            {
                Blocking = false
            };

            _socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));

            if (!SetSocketOption())
            {
                throw new Exception("Failed to set IOControl");
            }
        }

        private bool SetSocketOption()
        {
            var retValue = true;

            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, 1);

            var OUT = new byte[4];

            var retCode = OUT[0] + OUT[1] + OUT[2] + OUT[3];

            if (retCode != 0)
            {
                retValue = false;
            }

            return retValue;
        }

        private unsafe void Receive(byte[] buf, int len)
        {
            var e = new PacketArrivedEventArgs();

            fixed (byte* fixedBuf = buf)
            {
                var head = (IPHeader*)fixedBuf;
                e.HeaderLength = (uint)(head->ip_verlen & 0x0F) << 2;

                switch (head->ip_protocol)
                {
                    case 1: e.Protocol = "ICMP"; break;
                    case 2: e.Protocol = "IGMP"; break;
                    case 6: e.Protocol = "TCP"; break;
                    case 17: e.Protocol = "UDP"; break;
                    default: e.Protocol = "UNKNOWN"; break;
                }

                var tempVersion = (uint)(head->ip_verlen & 0xF0) >> 4;
                e.IPVersion = tempVersion.ToString();

                var tempIp = new IPAddress(head->ip_srcaddr);
                e.OriginationAddress = tempIp.ToString();
                tempIp = new IPAddress(head->ip_destaddr);
                e.DestinationAddress = tempIp.ToString();

                var tempSrcport = *(short*)&fixedBuf[e.HeaderLength];
                var tempDstport = *(short*)&fixedBuf[e.HeaderLength + 2];
                e.OriginationPort = IPAddress.NetworkToHostOrder(tempSrcport).ToString();
                e.DestinationPort = IPAddress.NetworkToHostOrder(tempDstport).ToString();

                e.PacketLength = (uint)len;
                e.MessageLength = (uint)len - e.HeaderLength;

                e.ReceiveBuffer = buf;

                Array.Copy(buf, 0, e.IPHeaderBuffer, 0, (int)e.HeaderLength);

                Array.Copy(buf, (int)e.HeaderLength, e.MessageBuffer, 0, (int)e.MessageLength);
            }

            OnPacketArrival(e);
        }

        public void Run()
        {
            _socket.BeginReceive(_receiveBufBytes, 0, Constants.len_receive_buf, SocketFlags.None, CallReceive, this);
        }
        
        private void CallReceive(IAsyncResult ar)
        {
            int receivedBytes;

            try
            {
                receivedBytes = _socket.EndReceive(ar);
            }
            catch (Exception)
            {
                receivedBytes = _receiveBufBytes.Length;
            }

            Receive(_receiveBufBytes, receivedBytes);
            Run();
        }

        public void Dispose()
        {
            if (_socket != null)
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    _socket.Shutdown(SocketShutdown.Both);
                }

                _socket.Close();
            }

            _socket?.Dispose();
        }
    }
}