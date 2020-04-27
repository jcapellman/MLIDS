using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MLIDS.lib.Containers;
using MLIDS.lib.DAL.Base;
using MLIDS.lib.ML.Objects;

using MongoDB.Driver;

namespace MLIDS.lib.DAL
{
    public class MongoDAL : BaseDAL
    {
        private readonly IMongoDatabase _db;

        private const string COLLECTION_NAME = "Packets";

        public MongoDAL(SettingsItem settings)
        {
            var mongoSettings = new MongoClientSettings()
            {
                Server = new MongoServerAddress(settings.DAL_HostIP, settings.DAL_HostPort)
            };

            var client = new MongoClient(mongoSettings);

            _db = client.GetDatabase(COLLECTION_NAME);
        }

        public override async Task<List<PayloadItem>> GetHostPacketsAsync(string hostName)
        {
            var collection = _db.GetCollection<PayloadItem>(COLLECTION_NAME);

            return await (await collection.FindAsync(a => a.HostName == hostName)).ToListAsync();
        }

        public override async Task<List<PayloadItem>> QueryPacketsAsync(System.Linq.Expressions.Expression<Func<PayloadItem, bool>> queryExpression)
        {
            var collection = _db.GetCollection<PayloadItem>(COLLECTION_NAME);

            return await(await collection.FindAsync(queryExpression)).ToListAsync();
        }

        public override async Task<bool> WritePacketAsync(PayloadItem packet)
        {
            var collection = _db.GetCollection<PayloadItem>(COLLECTION_NAME);

            await collection.InsertOneAsync(packet);

            return true;
        }
    }
}