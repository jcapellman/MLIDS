using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

using jcIDS.lib.Common;
using jcIDS.lib.Helpers;
using jcIDS.lib.Objects;

namespace jcIDS.lib.Managers
{
    public class SocketListener : IDisposable
    {
        private readonly byte[] _receiveBufBytes = new byte[Constants.len_receive_buf];
        private Socket _socket;
        
        public event PacketArrivedEventHandler PacketArrival;
        public delegate void PacketArrivedEventHandler(object sender, PacketArrivedEventArgs args);

        private bool _initialized;

        protected virtual void OnPacketArrival(PacketArrivedEventArgs e)
        {
            PacketArrival?.Invoke(this, e);
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

            if (networkInterface == null)
            {
                throw new Exception("Could not obtain a valid Network Interface");
            }

            var ipAddress = networkInterface.GetIPProperties().UnicastAddresses
                .FirstOrDefault(a => a.Address.AddressFamily == AddressFamily.InterNetwork);

            if (ipAddress == null)
            {
                throw new Exception($"Could not obtain IP Address from {networkInterface.Description}");
            }

            CreateAndBindSocket(ipAddress.Address);

            _initialized = true;
        }

        private void CreateAndBindSocket(IPAddress ip, int port = 0, ProtocolType protocol = ProtocolType.IP)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, protocol)
            {
                Blocking = false
            };

            _socket.Bind(new IPEndPoint(ip, port));

            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, 1);
        }

        private unsafe void Receive(byte[] buf, int len)
        {
            var e = new PacketArrivedEventArgs();

            fixed (byte * fixedBuf = buf)
            {
                var head = (IPHeader*)fixedBuf;

                e.HeaderLength = (uint)(head->ip_verlen & 0x0F) << 2;

                e.Protocol = head->ip_protocol.ToProtocolType();

                var tempVersion = (uint)(head->ip_verlen & 0xF0) >> 4;
                e.IPVersion = tempVersion.ToString();

                e.OriginationAddress = new IPAddress(head->ip_srcaddr).ToString();
                e.DestinationAddress = new IPAddress(head->ip_destaddr).ToString();

                var tempSrcPort = *(short*)&fixedBuf[e.HeaderLength];
                var tempDstPort = *(short*)&fixedBuf[e.HeaderLength + 2];

                e.OriginationPort = IPAddress.NetworkToHostOrder(tempSrcPort).ToString();
                e.DestinationPort = IPAddress.NetworkToHostOrder(tempDstPort).ToString();

                e.PacketLength = (uint)len;
                e.MessageLength = (uint)len - e.HeaderLength;

                e.ReceiveBuffer = buf;

                Array.Copy(buf, 0, e.IPHeaderBuffer, 0, (int)e.HeaderLength);

                Array.Copy(buf, (int)e.HeaderLength, e.MessageBuffer, 0, (int)e.MessageLength);
            }

            OnPacketArrival(e);
        }

        public (bool success, Exception exception) Run()
        {
            try
            {
                if (!_initialized)
                {
                    Initialize();
                }

                _socket.BeginReceive(_receiveBufBytes, 0, Constants.len_receive_buf, SocketFlags.None, CallReceive,
                    this);

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex);
            }
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
            if (_socket != null && Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                _socket.Shutdown(SocketShutdown.Both);
            }

            _socket?.Close();
            _socket?.Dispose();
        }
    }
}