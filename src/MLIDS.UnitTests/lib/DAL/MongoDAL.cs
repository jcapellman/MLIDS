using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace MLIDS.UnitTests.lib.DAL
{
    [TestClass]
    public class MongoDAL
    {
        [TestMethod]
        public async Task MongoDAL_NullTestAsync()
        {
            var mongo = new MLIDS.lib.DAL.MongoDAL();

            await mongo.GetHostPacketsAsync(null);
        }

        [TestMethod]
        public async Task MongoDAL_StringTestAsync()
        {
            var mongo = new MLIDS.lib.DAL.MongoDAL();

            await mongo.GetHostPacketsAsync("test");
        }

        [TestMethod]
        public async Task MongoDAL_WriteNullTestAsync()
        {
            var mongo = new MLIDS.lib.DAL.MongoDAL();

            await mongo.WritePacketAsync(null);
        }

        [TestMethod]
        public async Task MongoDAL_QueryNullTestAsync()
        {
            var mongo = new MLIDS.lib.DAL.MongoDAL();

            await mongo.QueryPacketsAsync(null);
        }
    }
}