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

            Assert.IsFalse(blackListManager.IsContained(a => a == null));   
        }

        [TestMethod]
        public void ContainsMiss()
        {
            var blackListManager = new BlackListManager();

            var result = blackListManager.IsContained(a => a.ResourceName == "TestNotFound");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetNullItem()
        {
            var blackListManager = new BlackListManager();

            var result = blackListManager.GetItem(a => a == null);

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

            var result = blackListManager.GetItem(a => a.ResourceName == item.ResourceName);

            Assert.IsNotNull(result);
        }
    }
}