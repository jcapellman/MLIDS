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
            typeof(ModelMetrics).ToPropertyList<ModelMetrics>(null);
        }

        [TestMethod]
        public void ExtensionMethods_ClassTestActualLabel()
        {
            typeof(ModelMetrics).ToPropertyList<ModelMetrics>("test");
        }

        [TestMethod]
        public void ExtensionMethods_StringTest()
        {
            typeof(string).ToPropertyList<ModelMetrics>(null);
        }
    }
}