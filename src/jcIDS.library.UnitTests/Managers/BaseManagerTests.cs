using jcIDS.library.core.Managers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jcIDS.library.UnitTests.Managers
{
    [TestClass]
    public class BaseManagerTests
    {
        [TestInitialize]
        public void Setup()
        {
            var coreManager = new CoreManager();

            coreManager.Initialize();
        }
    }
}