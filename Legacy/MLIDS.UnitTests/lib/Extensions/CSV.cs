using Microsoft.VisualStudio.TestTools.UnitTesting;

using MLIDS.lib.Containers;
using MLIDS.lib.Extensions;

namespace MLIDS.UnitTests
{
    [TestClass]
    public class CSV
    {
        [TestMethod]
        public void CSV_ClassTest()
        {
            var testObject = new ModelMetrics();

            var result = testObject.ToCSV<ModelMetrics>();

            Assert.IsNotNull(result);
        }
    }
}