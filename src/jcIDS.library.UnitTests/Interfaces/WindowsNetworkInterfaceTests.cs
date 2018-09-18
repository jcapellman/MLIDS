using jcIDS.library.windows.PlatformImplementations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jcIDS.library.UnitTests.Interfaces
{
    [TestClass]
    public class WindowsNetworkInterfaceTests
    {
        [TestMethod]
        public void ScanDevices()
        {
            var windowsNetworkInterface = new NetworkInterface();

            var results = windowsNetworkInterface.ScanDevices();
            
            Assert.IsTrue(results.Length > 0);
        }
    }
}