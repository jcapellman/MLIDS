using jcIDS.library.core.DAL.Objects;
using jcIDS.library.core.Managers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jcIDS.library.UnitTests.Managers
{
    [TestClass]
    public class NetworkDeviceManagerTests : BaseManagerTests
    {
        [TestMethod]
        public void ContainsNull()
        {
            var networkDeviceManager = new NetworkDeviceManager();

            Assert.IsFalse(networkDeviceManager.IsContained(a => a == null));
        }

        [TestMethod]
        public void ContainsMiss()
        {
            var networkDeviceManager = new NetworkDeviceManager();

            var result = networkDeviceManager.IsContained(a => a.ResourceName == "TestNotFound");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetNullItem()
        {
            var networkDeviceManager = new NetworkDeviceManager();

            var result = networkDeviceManager.GetItem(a => a == null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetActualItem()
        {
            var networkDeviceManager = new NetworkDeviceManager();

            var item = new NetworkDeviceObject
            {
                ResourceName = "Test"
            };

            var resultID = networkDeviceManager.AddItem(item);

            var result = networkDeviceManager.GetItem(a => a.ID == resultID);

            Assert.IsNotNull(result);
        }
    }
}