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
    }
}