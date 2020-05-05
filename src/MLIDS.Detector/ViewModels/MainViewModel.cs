using System;

using Microsoft.Win32;

using MLIDS.lib.ML;
using MLIDS.lib.Windows.ViewModels;

using PacketDotNet;

using SharpPcap;

namespace MLIDS.Detector.ViewModels
{
    public class MainViewModel : BaseCaptureMainViewModel
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

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

                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"MainViewModel::SelectModelFile - Threw an exception when attempting to use {LocationModelFile}: {ex}");
            }

            LocationModelFile = string.Empty;

            return false;
        }
        
        public override void PacketProcessing(CaptureEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);

                if (!packet.HasPayloadPacket)
                {
                    return;
                }

                var packetItem = GetPacket(packet, false);

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