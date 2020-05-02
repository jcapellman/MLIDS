using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MLIDS.UnitTests.lib.Containers
{
    [TestClass]
    public class SettingsItem
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SettingsItem_NullTest()
        {
            MLIDS.lib.Containers.SettingsItem.Load(null);
        }
    }
}