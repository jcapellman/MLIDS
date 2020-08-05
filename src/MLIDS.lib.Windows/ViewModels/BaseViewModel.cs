using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using MLIDS.lib.Containers;
using MLIDS.lib.DAL.Base;
using MLIDS.lib.Helpers;
using MLIDS.lib.ML.Objects;
using MLIDS.lib.Objects;
using PacketDotNet;

using SharpPcap;
using SharpPcap.Npcap;

namespace MLIDS.lib.Windows.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        public event EventHandler<string> OnFailedDAL;

        private List<BaseDAL> _dataLayers;

        public List<BaseDAL> DataLayers
        {
            get => _dataLayers;

            set
            {
                _dataLayers = value;

                OnPropertyChanged();
            }
        }

        private BaseDAL _selectedDataLayer;

        public BaseDAL SelectedDataLayer
        {
            get => _selectedDataLayer;

            set
            {
                _selectedDataLayer = value;

                if (_selectedDataLayer != null && _selectedDataLayer.IsSelectable && !_selectedDataLayer.Initialize())
                {
                    OnFailedDAL?.Invoke(this, _selectedDataLayer.Description);
                } else
                {
                    StartBtnEnabled = true;
                }

                StartButtonEnablement();

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

        private List<MLIDSDevice> _deviceList = new List<MLIDSDevice>();

        public List<MLIDSDevice> DeviceList
        {
            get => _deviceList;

            set
            {
                _deviceList = value;

                OnPropertyChanged();
            }
        }

        private MLIDSDevice _selectedDevice;

        public MLIDSDevice SelectedDevice
        {
            get => _selectedDevice;

            set
            {
                _selectedDevice = value;

                OnPropertyChanged();
            }
        }

        private bool _isRunning;

        public bool IsRunning
        {
            get => _isRunning;

            set
            {
                _isRunning = value;

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

        protected SettingsItem Settings;

        public BaseViewModel()
        {
            DeviceList = CaptureDeviceList.Instance.Where(a => a is NpcapDevice).OrderBy(a => a.Description).Select(b => new MLIDSDevice(b)).ToList();

            SelectedDevice = DeviceList.FirstOrDefault();

            StartBtnEnabled = false;
            StopBtnEnabled = false;
            DeviceSelectionEnabled = true;
            IsRunning = false;

            Settings = SettingsItem.Load();

            DataLayers = DALHelper.GetAvailableDALs(Settings);

            SelectedDataLayer = DataLayers.FirstOrDefault(a => !a.IsSelectable);
        }

        public abstract void StartButtonEnablement();

        public abstract void StartAction();

        public abstract void StopAction();

        protected static PayloadItem ToPayloadItem(string protocolType, IPPacket sourcePacket,
           InternetPacket payloadPacket, bool cleanTraffic) =>
           new PayloadItem(protocolType, sourcePacket, payloadPacket, cleanTraffic);

        protected static PayloadItem ToPayloadItem(string protocolType, IPPacket sourcePacket,
           TransportPacket payloadPacket, bool cleanTraffic) =>
           new PayloadItem(protocolType, sourcePacket, payloadPacket, cleanTraffic);

        protected PayloadItem GetPacket(Packet packet, bool isCleanTraffic)
        {
            IPPacket ipPacket = null;

            if (packet.PayloadPacket is IPv4Packet ipv4Packet)
            {
                ipPacket = ipv4Packet;
            } else if (packet.PayloadPacket is IPv6Packet ipv6Packet)
            {
                ipPacket = ipv6Packet;
            }

            if (ipPacket == null)
            {
                Log.Info("BaseViewModel::GetPacket - Packet was not IPv4 or IPv6");

                return null;
            }

            switch (ipPacket.Protocol)
            {
                case ProtocolType.Tcp:
                    var tcpPacket = packet.Extract<TcpPacket>();

                    return ToPayloadItem("TCP", ipPacket, tcpPacket, isCleanTraffic);
                case ProtocolType.Udp:
                    var udpPacket = packet.Extract<UdpPacket>();

                    return ToPayloadItem("UDP", ipPacket, udpPacket, isCleanTraffic);
                case ProtocolType.Icmp:
                    var icmpPacket = packet.Extract<IcmpV4Packet>();

                    return ToPayloadItem("ICMP", ipPacket, icmpPacket, isCleanTraffic);
            }

            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}