using MLIDS.lib.Common;

using SharpPcap;
using SharpPcap.Npcap;

namespace MLIDS.lib.Objects
{
    public class MLIDSDevice
    {
        public NpcapDevice CaptureDevice;

        public MLIDSDevice(ICaptureDevice captureDevice)
        {
            CaptureDevice = (NpcapDevice)captureDevice;
        }

        public delegate void PacketArrivalHandler(object sender, CaptureEventArgs e);
        public event PacketArrivalHandler OnPacketArrival;

        public string Description => $"{CaptureDevice.Description} ({CaptureDevice.Name})";

        public void StartCapture()
        {
            CaptureDevice.Open(OpenFlags.DataTransferUdp | OpenFlags.NoCaptureLocal, Constants.PACKET_READ_TIMEOUT_MS);

            CaptureDevice.OnPacketArrival += Device_OnPacketArrival;
            CaptureDevice.StartCapture();
        }

        public void StopCapture()
        {
            CaptureDevice.StopCapture();
            CaptureDevice.Close();
        }

        private void Device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            OnPacketArrival?.Invoke(this, e);
        }
    }
}