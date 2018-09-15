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

            Assert.IsFalse(whiteListManager.IsContained(null));
        }

        [TestMethod]
        public void ContainsMiss()
        {
            var whiteListManager = new WhiteListManager();

            var result = whiteListManager.IsContained("Test");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetNullItem()
        {
            var whiteListManager = new WhiteListManager();

            var result = whiteListManager.GetItem(0);

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

            var result = whiteListManager.GetItem(resultID);

            Assert.IsNotNull(result);
        }
    }
}
