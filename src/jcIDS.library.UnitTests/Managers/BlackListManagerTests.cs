using jcIDS.library.core.DAL.Objects;
using jcIDS.library.core.Managers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jcIDS.library.UnitTests.Managers
{
    [TestClass]
    public class BlackListManagerTests : BaseManagerTests
    {
        [TestMethod]
        public void ContainsNull()
        {
            var blackListManager = new BlackListManager();

            Assert.IsFalse(blackListManager.IsContained(null));   
        }

        [TestMethod]
        public void ContainsMiss()
        {
            var blackListManager = new BlackListManager();

            var result = blackListManager.IsContained("Test");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetNullItem()
        {
            var blackListManager = new BlackListManager();

            var result = blackListManager.GetItem(0);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetActualItem()
        {
            var blackListManager = new BlackListManager();

            var item = new BlackListObject
            {
                ResourceName = "Test"
            };

            var resultID = blackListManager.AddItem(item);

            var result = blackListManager.GetItem(resultID);

            Assert.IsNotNull(result);
        }
    }
}