using System;

using Microsoft.Win32;

using MLIDS.lib.ML;
using MLIDS.lib.ML.Objects;
using MLIDS.lib.ViewModels;

using PacketDotNet;

using SharpPcap;
using SharpPcap.Npcap;

namespace MLIDS.Detector.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private static Predictor _predictor;

        private string _locationModelFile;

        public string LocationModelFile
        {
            get => _locationModelFile;

            set
            {
                _locationModelFile = value;

                OnPropertyChanged();

                UpdateProtectButton();
            }
        }

        private void UpdateProtectButton()
        {
            StartBtnEnabled = !string.IsNullOrEmpty(LocationModelFile) &&
                               !IsRunning;
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

        private string SelectInputFile()
        {
            var openDialog = new OpenFileDialog
            {
                Filter = "ML Model |*.mdl",
                Title = "Select ML Model"
            };

            openDialog.ShowDialog();

            return openDialog.FileName;
        }

        public bool SelectModelFile()
        {
            LocationModelFile = SelectInputFile() ?? LocationModelFile;

            if (string.IsNullOrEmpty(LocationModelFile))
            {
                return true;
            }

            try
            {
                _predictor = new Predictor(LocationModelFile);

                LocationModelFile = string.Empty;

                return true;
            } catch (Exception) { }

            return false;
        }

        private static PayloadItem ToPayloadItem(string protocolType, IPv4Packet sourcePacket, TransportPacket payloadPacket) =>
            new PayloadItem(protocolType, sourcePacket, payloadPacket, false);

        private PayloadItem GetPacket(Packet packet)
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

                    return ToPayloadItem("TCP", ipPacket, tcpPacket);
                case ProtocolType.Udp:
                    var udpPacket = packet.Extract<PacketDotNet.UdpPacket>();

                    return ToPayloadItem("UDP", ipPacket, udpPacket);
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

                var packetItem = GetPacket(packet);

                if (packetItem == null)
                {
                    return;
                }

                var result = _predictor.Predict(packetItem);

                if (!result.Label)
                {
                    Packets.Add($"{packetItem.DestinationIPAddress}:{packetItem.DestinationPort} was found to be malicious at a {result.Score} confidence");
                }
            });
        }    
    }
}