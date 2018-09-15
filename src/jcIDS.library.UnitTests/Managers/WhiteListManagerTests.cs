using jcIDS.library.core.DAL.Objects;
using jcIDS.library.core.Managers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jcIDS.library.UnitTests.Managers
{
    [TestClass]
    public class WhiteListManagerTests : BaseManagerTests
    {
        [TestMethod]
        public void ContainsNull()
        {
            var whiteListManager = new WhiteListManager();

            Assert.IsFalse(whiteListManager.IsContained(a => a == null));
        }

        [TestMethod]
        public void ContainsMiss()
        {
            var whiteListManager = new WhiteListManager();

            var result = whiteListManager.IsContained(a => a.ResourceName == "TestNotFound");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetNullItem()
        {
            var whiteListManager = new WhiteListManager();

            var result = whiteListManager.GetItem(a => a.ID == 0);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetActualItem()
        {
            var whiteListManager = new WhiteListManager();

            var item = new WhiteListObject
            {
                ResourceName = "Test"
            };

            var resultID = whiteListManager.AddItem(item);

            var result = whiteListManager.GetItem(a => a.ID == resultID);

            Assert.IsNotNull(result);
        }
    }
}
