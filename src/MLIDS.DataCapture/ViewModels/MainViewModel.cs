using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

using SharpPcap;
using SharpPcap.Npcap;

namespace MLIDS.DataCapture.ViewModels
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
        }

        public void StartCapture()
        {
            StartBtnEnabled = false;
            StopBtnEnabled = true;

            if (SelectedDevice is NpcapDevice)
            {
                var nPcap = SelectedDevice as NpcapDevice;

                nPcap?.Open(SharpPcap.Npcap.OpenFlags.DataTransferUdp | SharpPcap.Npcap.OpenFlags.NoCaptureLocal, 1000);
            }

            SelectedDevice.OnPacketArrival += device_OnPacketArrival;
            SelectedDevice.StartCapture();
        }

        public void StopCapture()
        {
            try
            {
                SelectedDevice.OnPacketArrival -= device_OnPacketArrival;

                SelectedDevice.StopCapture();
                SelectedDevice.Close();
            } catch (Exception) { }

            StartBtnEnabled = true;
            StopBtnEnabled = false;
        }

        private void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
            {
                Packets.Add(e.Packet.ToString());
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}