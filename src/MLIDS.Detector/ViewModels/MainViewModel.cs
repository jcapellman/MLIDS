using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

using Microsoft.Win32;
using MLIDS.lib.Extensions;
using MLIDS.lib.ML.Objects;
using PacketDotNet;
using SharpPcap;
using SharpPcap.Npcap;

namespace MLIDS.Detector.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool _startBtnEnabled;

        public bool StartBtnEnabled
        {
            get => _startBtnEnabled;

            set
            {
                _startBtnEnabled = value;

                OnPropertyChanged();
            }
        }

        private bool _deviceSelectionEnabled;

        public bool DeviceSelectionEnabled
        {
            get => _deviceSelectionEnabled;

            set
            {
                _deviceSelectionEnabled = value;

                OnPropertyChanged();
            }
        }

        private bool _stopBtnEnabled;

        public bool StopBtnEnabled
        {
            get => _stopBtnEnabled;

            set
            {
                _stopBtnEnabled = value;

                OnPropertyChanged();
            }
        }

        private List<ICaptureDevice> _deviceList = new List<ICaptureDevice>();

        public List<ICaptureDevice> DeviceList
        {
            get => _deviceList;

            set
            {
                _deviceList = value;

                OnPropertyChanged();
            }
        }

        private ICaptureDevice _selectedDevice;

        public ICaptureDevice SelectedDevice
        {
            get => _selectedDevice;

            set
            {
                _selectedDevice = value;

                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _packets = new ObservableCollection<string>();

        public ObservableCollection<string> Packets
        {
            get => _packets;

            set
            {
                _packets = value;

                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            DeviceList = CaptureDeviceList.Instance.ToList();

            SelectedDevice = DeviceList.FirstOrDefault();

            StartBtnEnabled = true;
            StopBtnEnabled = false;
            DeviceSelectionEnabled = true;
        }

        public void StartCapture()
        {
            StartBtnEnabled = false;
            StopBtnEnabled = true;
            DeviceSelectionEnabled = false;
            
            if (SelectedDevice is NpcapDevice)
            {
                var nPcap = SelectedDevice as NpcapDevice;

                nPcap?.Open(OpenFlags.DataTransferUdp | OpenFlags.NoCaptureLocal, 1000);
            }

            SelectedDevice.OnPacketArrival += Device_OnPacketArrival;
            SelectedDevice.StartCapture();
        }

        public void StopCapture()
        {
            try
            {
                SelectedDevice.OnPacketArrival -= Device_OnPacketArrival;

                SelectedDevice.StopCapture();
                SelectedDevice.Close();
            }
            catch (Exception) { }

            StartBtnEnabled = true;
            StopBtnEnabled = false;
            DeviceSelectionEnabled = true;
        }

        private static string ToCSV(string protocolType, IPv4Packet sourcePacket, TransportPacket payloadPacket, bool cleanTraffic) =>
            new PayloadItem(protocolType, sourcePacket, payloadPacket, cleanTraffic).ToCSV<PayloadItem>();

        private string GetPacket(Packet packet)
        {
            if (!(packet.PayloadPacket is IPv4Packet))
            {
                return string.Empty;
            }

            var ipPacket = (IPv4Packet)packet.PayloadPacket;

            switch (ipPacket.Protocol)
            {
                case ProtocolType.Tcp:
                    var tcpPacket = packet.Extract<PacketDotNet.TcpPacket>();

                    return ToCSV("TCP", ipPacket, tcpPacket, IsCleanTraffic);
                case ProtocolType.Udp:
                    var udpPacket = packet.Extract<PacketDotNet.UdpPacket>();

                    return ToCSV("UDP", ipPacket, udpPacket, IsCleanTraffic);
            }

            return null;
        }

        private void Device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);

                if (!packet.HasPayloadPacket)
                {
                    return;
                }

                var line = GetPacket(packet);

                if (string.IsNullOrEmpty(line))
                {
                    return;
                }

                if (!string.IsNullOrEmpty(_fileName))
                {
                    File.AppendAllLines(_fileName, new List<string> { line });

                    return;
                }

                Packets.Add(line);
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}