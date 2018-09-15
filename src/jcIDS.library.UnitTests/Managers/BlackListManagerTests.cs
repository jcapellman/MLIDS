using System;

using jcIDS.library.core.Managers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jcIDS.library.UnitTests.Managers
{
    [TestClass]
    public class BlackListManagerTests
    {
        [TestInitialize]
        public void Setup()
        {
            var coreManager = new CoreManager();

            coreManager.Initialize();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ContainsNull()
        {
            var blackListManager = new BlackListManager();

            blackListManager.IsContained(null);   
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