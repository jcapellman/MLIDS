using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MLIDS.lib.Containers;
using MLIDS.lib.DAL.Base;
using MLIDS.lib.ML.Objects;

using MongoDB.Driver;

namespace MLIDS.lib.DAL
{
    public class MongoDal(SettingsItem settings) : BaseDal(settings)
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        private IMongoDatabase _db;

        private const string COLLECTION_NAME = "Packets";

        public override string Description => "MongoDB";

        public override bool IsSelectable => true;

        public override bool Initialize()
        {
            try
            {
                var mongoSettings = new MongoClientSettings()
                {
                    Server = new MongoServerAddress(settingsItem.DAL_HostIP, settingsItem.DAL_HostPort)
                };

                var client = new MongoClient(mongoSettings);

                _db = client.GetDatabase(COLLECTION_NAME);

                Log.Debug($"MongoDal::Initialize - Database Loaded ({settingsItem.DAL_HostIP}:{settingsItem.DAL_HostPort})");

                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"MongoDal::Initialize - Failed to connect due to: {ex}");

                return false;
            }
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
                Log.Error($"MongoDal::QueryPacketsAsync - Query Expression was null");

                throw new ArgumentNullException(nameof(queryExpression));
            }

            var collection = _db.GetCollection<PayloadItem>(COLLECTION_NAME);

            return await(await collection.FindAsync(queryExpression)).ToListAsync();
        }

        public override async Task<bool> WritePacketAsync(PayloadItem packet)
        {
            if (packet == null)
            {
                Log.Error($"MongoDal::WritePacketAsync - packet was null");

                throw new ArgumentNullException(nameof(packet));
            }

            var collection = _db.GetCollection<PayloadItem>(COLLECTION_NAME);

            await collection.InsertOneAsync(packet);

            return true;
        }
    }
}