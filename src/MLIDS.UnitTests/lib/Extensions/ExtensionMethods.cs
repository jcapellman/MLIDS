using Microsoft.VisualStudio.TestTools.UnitTesting;

using MLIDS.lib.Containers;
using MLIDS.lib.Extensions;

namespace MLIDS.UnitTests.lib.Extensions
{
    [TestClass]
    public class ExtensionMethods
    {
        [TestMethod]
        public void ExtensionMethods_ClassTestNullLabel()
        {
            var result = typeof(ModelMetrics).ToPropertyList<ModelMetrics>(null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ExtensionMethods_ClassTestActualLabel()
        {
            var result = typeof(ModelMetrics).ToPropertyList<ModelMetrics>("test");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ExtensionMethods_StringTest()
        {
            var result = typeof(string).ToPropertyList<ModelMetrics>(null);

            Assert.IsNotNull(result);
        }
    }
}