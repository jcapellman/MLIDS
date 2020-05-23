using MLIDS.lib.ML.Objects;
using MLIDS.lib.Windows.ViewModels;

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
            StartBtnEnabled = false;
        }

        public override void PacketProcessing(PayloadItem payloadItem)
        {
            if (EnableSaveStream)
            {
                Log.Debug($"Saving Packet to DAL: {payloadItem}");

                SelectedDataLayer.WritePacketAsync(payloadItem);
            }
            else
            {
                Log.Debug($"Saving Packet to Collection: {payloadItem}");

                Packets.Add(payloadItem.ToString());
            }
        }
    }
}