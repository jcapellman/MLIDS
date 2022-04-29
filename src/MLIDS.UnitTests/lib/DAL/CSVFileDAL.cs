using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MLIDS.UnitTests.lib.DAL
{
    [TestClass]
    public class CSVFileDAL
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CSVFileDAL_NullTestAsync()
        {
            var csvDal = new MLIDS.lib.DAL.CSVFileDAL(new MLIDS.lib.Containers.SettingsItem());

            await csvDal.GetHostPacketsAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public async Task CSVFileDAL_StringTestAsync()
        {
            var csvDal = new MLIDS.lib.DAL.CSVFileDAL(new MLIDS.lib.Containers.SettingsItem());

            await csvDal.GetHostPacketsAsync("test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CSVFileDAL_WriteNullTestAsync()
        {
            var csvDal = new MLIDS.lib.DAL.CSVFileDAL(new MLIDS.lib.Containers.SettingsItem());

            var result = await csvDal.WritePacketAsync(null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task CSVFileDAL_WriteEmptyTestAsync()
        {
            var csvDal = new MLIDS.lib.DAL.CSVFileDAL(new MLIDS.lib.Containers.SettingsItem());

            var result = await csvDal.WritePacketAsync(new MLIDS.lib.ML.Objects.PayloadItem());

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public async Task CSVFileDAL_WriteNotEmptyTestAsync()
        {
            var csvDal = new MLIDS.lib.DAL.CSVFileDAL(new MLIDS.lib.Containers.SettingsItem());

            var result = await csvDal.WritePacketAsync(new MLIDS.lib.ML.Objects.PayloadItem
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
        public async Task CSVFileDAL_GetTestAsync()
        {
            var csvDal = new MLIDS.lib.DAL.CSVFileDAL(new MLIDS.lib.Containers.SettingsItem());

            var initResult = csvDal.Initialize();

            var writeResult = await csvDal.WritePacketAsync(new MLIDS.lib.ML.Objects.PayloadItem
            {
               DecodedPayload = "Testing",
               DestinationIPAddress = "127.0.0.1",
               DestinationPort = 30,
               Guid = Guid.NewGuid(),
               HeaderSize = 20,
               HostName = "Testo",
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

            Assert.IsTrue(writeResult);

            var result = await csvDal.GetHostPacketsAsync("Testo");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CSVFileDAL_QueryNullTestAsync()
        {
            var csvDal = new MLIDS.lib.DAL.CSVFileDAL(new MLIDS.lib.Containers.SettingsItem());

            var result = await csvDal.QueryPacketsAsync(null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CSVFileDAL_QueryEmptyTestAsync()
        {
            var csvDal = new MLIDS.lib.DAL.CSVFileDAL(new MLIDS.lib.Containers.SettingsItem());

            var result = await csvDal.QueryPacketsAsync(a => a.Label);

            Assert.IsTrue(!result.Any());
        }
    }
}