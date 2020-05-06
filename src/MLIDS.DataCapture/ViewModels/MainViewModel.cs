using System;

using MLIDS.lib.Windows.ViewModels;

using PacketDotNet;

using SharpPcap;

namespace MLIDS.DataCapture.ViewModels
{
    public class MainViewModel : BaseCaptureMainViewModel
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        private bool _enableSaveStream;

        public bool EnableSaveStream
        {
            get => _enableSaveStream;

            set
            {
                _enableSaveStream = value;

                OnPropertyChanged();
            }
        }

        private bool _isCleanTraffic;

        public bool IsCleanTraffic
        {
            get => _isCleanTraffic;

            set
            {
                _isCleanTraffic = value;

                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            StartBtnEnabled = true;
        }

        public override void PacketProcessing(CaptureEventArgs e)
        {
            if (e == null)
            {
                Log.Error("MainViewModel::PacketProcessing - e is null");

                throw new ArgumentNullException(nameof(e));
            }

            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);

                if (!packet.HasPayloadPacket)
                {
                    Log.Info("Packet has no payload");

                    return;
                }

                var packetItem = GetPacket(packet, false);

                if (packetItem == null)
                {
                    Log.Info("PacketItem was null");

                    return;
                }

                if (EnableSaveStream)
                {
                    Log.Debug($"Saving Packet to DAL: {packetItem}");

                    _dataStorage.WritePacketAsync(packetItem);
                }
                else
                {
                    Log.Debug($"Saving Packet to Collection: {packetItem}");

                    Packets.Add(packetItem.ToString());
                }
            });
        }
    }
}