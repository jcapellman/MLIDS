using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MLIDS.UnitTests.lib.DAL
{
    [TestClass]
    public class JsonDAL
    {
        [TestMethod]
        public async Task JsonDAL_NullTestAsync()
        {
            var jsonDal = new MLIDS.lib.DAL.JsonFileDAL(new MLIDS.lib.Containers.SettingsItem());

            var result = await jsonDal.GetHostPacketsAsync(null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task JsonDAL_StringTestAsync()
        {
            var jsonDal = new MLIDS.lib.DAL.JsonFileDAL(new MLIDS.lib.Containers.SettingsItem());

            var result = await jsonDal.GetHostPacketsAsync("test");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task JsonDAL_WriteNullTestAsync()
        {
            var jsonDal = new MLIDS.lib.DAL.JsonFileDAL(new MLIDS.lib.Containers.SettingsItem());

            await jsonDal.WritePacketAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task JsonDAL_QueryNullTestAsync()
        {
            var jsonDal = new MLIDS.lib.DAL.JsonFileDAL(new MLIDS.lib.Containers.SettingsItem());

            await jsonDal.QueryPacketsAsync(null);
        }

        [TestMethod]
        public async Task JsonDAL_QueryEmptyTestAsync()
        {
            var jsonDal = new MLIDS.lib.DAL.JsonFileDAL(new MLIDS.lib.Containers.SettingsItem());

            var result = await jsonDal.QueryPacketsAsync(a => a.Label);

            Assert.IsTrue(!result.Any());
        }

        [TestMethod]
        public void JsonDAL_InitializeTest()
        {
            var jsonDal = new MLIDS.lib.DAL.JsonFileDAL(new MLIDS.lib.Containers.SettingsItem());

            var result = jsonDal.Initialize();

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task JSONDAL_NullFileNameTestAsync()
        {
            var jsonDal = new MLIDS.lib.DAL.JsonFileDAL(new MLIDS.lib.Containers.SettingsItem());

            var result = await jsonDal.WritePacketAsync(new MLIDS.lib.ML.Objects.PayloadItem
            {
                DecodedPayload = "Testing",
                DestinationIPAddress = "127.0.0.1",
                DestinationPort = 30,
                Guid = Guid.NewGuid(),
                HeaderSize = 20,
                HostName = "Continental",
                IsClean = true,
                IsEncrypted = true,
                PacketContent = "There is no spoon",
                PayloadSize = 20,
                ProtocolType = "tcp",
                SourceIPAddress = "127.0.0.1",
                SourcePort = 30,
                Timestamp = DateTime.UtcNow,
                Version = 1
            });

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task JSONDAL_WriteNotEmptyTestAsync()
        {
            var jsonDal = new MLIDS.lib.DAL.JsonFileDAL(new MLIDS.lib.Containers.SettingsItem
            {
                DAL_FileName = "Testo.json"
            });

            jsonDal.Initialize();

            var result = await jsonDal.WritePacketAsync(new MLIDS.lib.ML.Objects.PayloadItem
            {
                DecodedPayload = "Testing",
                DestinationIPAddress = "127.0.0.1",
                DestinationPort = 30,
                Guid = Guid.NewGuid(),
                HeaderSize = 20,
                HostName = "Continental",
                IsClean = true,
                IsEncrypted = true,
                PacketContent = "There is no spoon",
                PayloadSize = 20,
                ProtocolType = "tcp",
                SourceIPAddress = "127.0.0.1",
                SourcePort = 30,
                Timestamp = DateTime.UtcNow,
                Version = 1
            });

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task JSONDAL_QueryTestAsync()
        {
            var jsonDal = new MLIDS.lib.DAL.JsonFileDAL(new MLIDS.lib.Containers.SettingsItem
            {
                DAL_FileName = "Testo.json"
            });

            jsonDal.Initialize();

            var result = await jsonDal.WritePacketAsync(new MLIDS.lib.ML.Objects.PayloadItem
            {
                DecodedPayload = "Testing",
                DestinationIPAddress = "127.0.0.1",
                DestinationPort = 30,
                Guid = Guid.NewGuid(),
                HeaderSize = 20,
                HostName = "Continental",
                IsClean = true,
                IsEncrypted = true,
                PacketContent = "There is no spoon",
                PayloadSize = 20,
                ProtocolType = "tcp",
                SourceIPAddress = "127.0.0.1",
                SourcePort = 30,
                Timestamp = DateTime.UtcNow,
                Version = 1
            });

            var queryResult = await jsonDal.QueryPacketsAsync(a => a.HostName == "Continental");

            Assert.IsNotNull(queryResult);
            Assert.IsTrue(queryResult.Any());
        }
    }
}