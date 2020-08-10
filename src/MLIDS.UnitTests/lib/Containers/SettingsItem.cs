using System;
using System.Text.Json;

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

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SettingsItem_NullSaveTest()
        {
            MLIDS.lib.Containers.SettingsItem.Save(null);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonException))]
        public void SettingsItem_BadJSONSaveTest()
        {
            MLIDS.lib.Containers.SettingsItem.Save("bad");
        }

        [TestMethod]
        public void SettingsItem_GoodJSONSaveTest()
        {
            var setting = MLIDS.lib.Containers.SettingsItem.Save("{\"DAL_HostIP\":\"192.168.0.1\",\"DAL_HostPort\":0,\"DAL_FileName\":\"\"}");

            Assert.AreEqual("192.168.0.1", setting.DAL_HostIP);
        }
    }
}