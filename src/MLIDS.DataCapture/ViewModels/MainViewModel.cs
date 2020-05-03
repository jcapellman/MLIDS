using MLIDS.lib.Windows.ViewModels;

using PacketDotNet;

using SharpPcap;
using System;

namespace MLIDS.DataCapture.ViewModels
{
    public class MainViewModel : BaseCaptureMainViewModel
    {
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
                throw new ArgumentNullException(nameof(e));
            }

            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                var packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);

                if (!packet.HasPayloadPacket)
                {
                    return;
                }

                var payloadItem = GetPacket(packet, IsCleanTraffic);

                if (payloadItem == null)
                {
                    return;
                }

                if (EnableSaveStream)
                {
                    _dataStorage.WritePacketAsync(payloadItem);
                }
                else
                {
                    Packets.Add(payloadItem.ToString());
                }
            });
        }
    }
}