using MLIDS.lib.Common;

using SharpPcap;
using SharpPcap.LibPcap;

namespace MLIDS.lib.Objects
{
    public class MLIDSDevice
    {
        private readonly PcapDevice CaptureDevice;

        public MLIDSDevice(ICaptureDevice captureDevice)
        {
            CaptureDevice = (PcapDevice)captureDevice;
        }

        public delegate void PacketArrivalHandler(object sender, PacketCapture e);
        public event PacketArrivalHandler OnPacketArrival;

        public string Description => $"{CaptureDevice.Description} ({CaptureDevice.Name})";

        public void StartCapture()
        {
            CaptureDevice.Open(new DeviceConfiguration
            {
                ReadTimeout = Constants.PACKET_READ_TIMEOUT_MS, 
                Mode = DeviceModes.DataTransferUdp | DeviceModes.NoCaptureLocal
            });
            
            CaptureDevice.OnPacketArrival += Device_OnPacketArrival;
            CaptureDevice.StartCapture();
        }

        private void Device_OnPacketArrival(object sender, PacketCapture e)
        {
            OnPacketArrival?.Invoke(this, e);
        }

        public void StopCapture()
        {
            CaptureDevice.StopCapture();
            CaptureDevice.Close();
        }
    }
}