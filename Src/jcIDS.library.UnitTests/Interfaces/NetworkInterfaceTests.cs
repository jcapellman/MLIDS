using jcIDS.library.core.PlatformImplementations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jcIDS.library.UnitTests.Interfaces
{
    [TestClass]
    public class NetworkInterfaceTests
    {
        [TestMethod]
        public void IsOnlineCheck()
        {
            var networkInterface = new NetworkInterface();

            Assert.IsTrue(networkInterface.IsOnline());
        }

        [TestMethod]
        public void ScanDevices()
        {
            var windowsNetworkInterface = new NetworkInterface();

            var results = windowsNetworkInterface.ScanDevices();
            
            Assert.IsTrue(results.Count > 0);
        }
    }
}