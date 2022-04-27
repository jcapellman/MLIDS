using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace MLIDS.UnitTests.lib.Helpers
{
    [TestClass]
    public class DALHelper
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DALHelperTests_NullSettings()
        {
            _ = MLIDS.lib.Helpers.DALHelper.GetAvailableDALs(null);
        }

        [TestMethod]
        public void DALHelperTests_EmptySettings()
        {
            var result = MLIDS.lib.Helpers.DALHelper.GetAvailableDALs(new MLIDS.lib.Containers.SettingsItem());

            Assert.IsTrue(result.Count == 5);
        }
    }
}