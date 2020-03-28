using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;

using Microsoft.Win32;

using PacketDotNet;

using SharpPcap;
using SharpPcap.Npcap;

namespace MLIDS.DataCapture.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _fileName;

        private bool _enableFileStream;

        public bool EnableFileStream
        {
            get => _enableFileStream;

            set
            {
                _enableFileStream = value;

                OnPropertyChanged();
            }
        }

        private bool _chkBxSaveEnabled;

        public bool ChkBxSaveEnabled
        {
            get => _chkBxSaveEnabled;

            set
            {
                _chkBxSaveEnabled = value;

                OnPropertyChanged();
            }
        }

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
            ChkBxSaveEnabled = true;
        }

        public void StartCapture()
        {
            StartBtnEnabled = false;
            StopBtnEnabled = true;
            DeviceSelectionEnabled = false;
            ChkBxSaveEnabled = false;

            if (EnableFileStream)
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "LOG File|*.log", Title = "Save a Log File"
                };

                saveDialog.ShowDialog();

                if (string.IsNullOrEmpty(saveDialog.FileName))
                {
                    StopCapture();

                    return;
                }

                _fileName = saveDialog.FileName;
            }

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
            } catch (Exception) { }

            StartBtnEnabled = true;
            StopBtnEnabled = false;
            DeviceSelectionEnabled = true;
            ChkBxSaveEnabled = true;
        }

        private static string PacketToString(byte[] packetContent) => BitConverter.ToString(packetContent);

        private string GetPacket(Packet packet)
        {
            if (!(packet.PayloadPacket is IPv4Packet))
            {
                return string.Empty;
            }

            var ipPacket = (IPv4Packet) packet.PayloadPacket;

            var line = string.Empty;

            switch (ipPacket.Protocol)
            {
                case ProtocolType.Tcp:
                    var tcpPacket = packet.Extract<PacketDotNet.TcpPacket>();
                    
                    line =
                        $"TCP: {ipPacket.SourceAddress}:{tcpPacket.SourcePort} to {ipPacket.DestinationAddress}:{tcpPacket.DestinationPort} " +
                        $"- Packet Length: {tcpPacket.TotalPacketLength} - Packet: {PacketToString(tcpPacket.PayloadData)}";
                    break;
                case ProtocolType.Udp:
                    var udpPacket = packet.Extract<PacketDotNet.UdpPacket>();

                    line =
                        $"UDP: {ipPacket.SourceAddress}:{udpPacket.SourcePort} to {ipPacket.DestinationAddress}:{udpPacket.DestinationPort} " +
                        $"- Packet Length: {udpPacket.TotalPacketLength} - Packet: {PacketToString(udpPacket.PayloadData)}";
                    break;
            }

            return line;
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
                    File.AppendAllText(_fileName, line);
                }

                Packets.Add(line);
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}