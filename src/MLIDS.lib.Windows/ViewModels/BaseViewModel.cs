using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using MLIDS.lib.Containers;
using MLIDS.lib.DAL;
using MLIDS.lib.DAL.Base;
using MLIDS.lib.ML.Objects;

using PacketDotNet;

using SharpPcap;

namespace MLIDS.lib.Windows.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected BaseDAL _dataStorage;

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
            DeviceList = CaptureDeviceList.Instance.ToList();

            SelectedDevice = DeviceList.FirstOrDefault();

            StartBtnEnabled = false;
            StopBtnEnabled = false;
            DeviceSelectionEnabled = true;
            IsRunning = false;

            Settings = SettingsItem.Load();

            _dataStorage = new MongoDAL(Settings);
        }

        public abstract void StartAction();

        public abstract void StopAction();

        protected static PayloadItem ToPayloadItem(string protocolType, IPv4Packet sourcePacket,
           TransportPacket payloadPacket, bool cleanTraffic) =>
           new PayloadItem(protocolType, sourcePacket, payloadPacket, cleanTraffic);

        protected PayloadItem GetPacket(Packet packet, bool isCleanTraffic)
        {
            if (!(packet.PayloadPacket is IPv4Packet))
            {
                return null;
            }

            var ipPacket = (IPv4Packet)packet.PayloadPacket;

            switch (ipPacket.Protocol)
            {
                case ProtocolType.Tcp:
                    var tcpPacket = packet.Extract<PacketDotNet.TcpPacket>();

                    return ToPayloadItem("TCP", ipPacket, tcpPacket, isCleanTraffic);
                case ProtocolType.Udp:
                    var udpPacket = packet.Extract<PacketDotNet.UdpPacket>();

                    return ToPayloadItem("UDP", ipPacket, udpPacket, isCleanTraffic);
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
