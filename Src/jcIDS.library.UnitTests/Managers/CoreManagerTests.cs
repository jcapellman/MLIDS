using jcIDS.library.core.Managers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jcIDS.library.UnitTests.Managers
{
    [TestClass]
    public class CoreManagerTests
    {
        [TestMethod]
        public void Initialize()
        {
            var coreManager = new CoreManager();

            var result = coreManager.Initialize();

            Assert.IsTrue(result);
        }
    }
}