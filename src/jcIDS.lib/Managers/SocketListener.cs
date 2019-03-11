using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

using jcIDS.lib.Common;
using jcIDS.lib.CommonObjects;
using jcIDS.lib.Helpers;
using jcIDS.lib.Objects;

namespace jcIDS.lib.Managers
{
    public class SocketListener : IDisposable
    {
        private readonly byte[] _receiveBufBytes = new byte[Constants.RECEIVE_BUFFER_LENGTH];
        private Socket _socket;
        
        public event EventHandler<Packet> PacketArrival;
        
        private bool _initialized;

        protected virtual void OnPacketArrival(Packet packet)
        {
            PacketArrival?.Invoke(this, packet);
        }

        private static (NetworkInterface networkInterface, Exception exception) GetNetworkInterface() {
            try
            {
                var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                return (networkInterfaces.FirstOrDefault(a =>
                    a.OperationalStatus == OperationalStatus.Up &&
                    (a.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                     a.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet)), null);
            }
            catch (NetworkInformationException exception)
            {
                return (null, exception);
            }
        }

        private void Initialize()
        {
            var (networkInterface, exception) = GetNetworkInterface();

            if (networkInterface == null)
            {
                throw new Exception($"Could not obtain a valid Network Interface due to {exception}");
            }

            var ipAddress = networkInterface.GetIPProperties().UnicastAddresses
                .FirstOrDefault(a => a.Address.AddressFamily == AddressFamily.InterNetwork);

            if (ipAddress == null)
            {
                throw new Exception($"Could not obtain IP Address from {networkInterface.Description}");
            }

            var socketException = CreateAndBindSocket(ipAddress.Address);

            if (socketException != null)
            {
                throw new Exception($"Error while binding socket {socketException}");
            }

            _initialized = true;
        }

        private Exception CreateAndBindSocket(IPAddress ip, int port = 0, ProtocolType protocol = ProtocolType.IP)
        {
            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, protocol)
                {
                    Blocking = false
                };

                _socket.Bind(new IPEndPoint(ip, port));

                _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, 1);

                return null;
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        private unsafe void Receive(byte[] buf, int len)
        {
            var e = new Packet();

            fixed (byte * fixedBuf = buf)
            {
                var head = (IPHeader*)fixedBuf;

                e.HeaderLength = (uint)(head->ip_verlen & 0x0F) << 2;

                e.Protocol = head->ip_protocol.ToProtocolType();

                e.IPVersion = ((uint)(head->ip_verlen & 0xF0) >> 4).ToString();

                e.OriginationAddress = new IPAddress(head->ip_srcaddr).ToString();
                e.DestinationAddress = new IPAddress(head->ip_destaddr).ToString();

                e.OriginationPort = IPAddress.NetworkToHostOrder(*(short*)&fixedBuf[e.HeaderLength]).ToString();
                e.DestinationPort = IPAddress.NetworkToHostOrder(Math.Abs(*(short*)&fixedBuf[e.HeaderLength + 2])).ToString();

                e.PacketLength = (uint)len;
                e.MessageLength = (uint)len - e.HeaderLength;

                e.ReceiveBuffer = buf;

                e.IPHeaderBuffer = new byte[Constants.RECEIVE_BUFFER_LENGTH];
                e.MessageBuffer = new byte[Constants.RECEIVE_BUFFER_LENGTH];

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

                _socket.BeginReceive(_receiveBufBytes, 0, Constants.RECEIVE_BUFFER_LENGTH, SocketFlags.None, CallReceive,
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