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
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        private readonly IMongoDatabase _db;

        private const string COLLECTION_NAME = "Packets";

        public MongoDAL() : this(new SettingsItem()) { }

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
            if (queryExpression == null)
            {
                Log.Error($"MongoDAL::QueryPacketsAsync - Query Expression was null");

                throw new ArgumentNullException(nameof(queryExpression));
            }

            var collection = _db.GetCollection<PayloadItem>(COLLECTION_NAME);

            return await(await collection.FindAsync(queryExpression)).ToListAsync();
        }

        public override async Task<bool> WritePacketAsync(PayloadItem packet)
        {
            if (packet == null)
            {
                throw new ArgumentNullException(nameof(packet));
            }

            var collection = _db.GetCollection<PayloadItem>(COLLECTION_NAME);

            await collection.InsertOneAsync(packet);

            return true;
        }
    }
}