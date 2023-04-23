using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using LiteDB;

using MLIDS.lib.Containers;
using MLIDS.lib.DAL.Base;
using MLIDS.lib.ML.Objects;

namespace MLIDS.lib.DAL
{
    public class LiteDBDAL : BaseDAL
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        private string _connectionString;

        public override string Description => "LiteDB";

        public override bool IsSelectable => true;

        public LiteDBDAL(SettingsItem settings) : base(settings) { }
    
        public override Task<List<PayloadItem>> GetHostPacketsAsync(string hostName) => 
            Task.Run(() =>
            {
                using var db = new LiteDatabase(_connectionString);

                return db.GetCollection<PayloadItem>().Find(a => a.HostName == hostName).ToList();
            });

        public override Task<List<PayloadItem>> QueryPacketsAsync(Expression<Func<PayloadItem, bool>> queryExpression) => 
            Task.Run(() =>
            {
                if (queryExpression is null)
                {
                    Log.Error($"LiteDBDAL::QueryPacketsAsync - Query Expression was null");

                    throw new ArgumentNullException(nameof(queryExpression));
                }

                using var db = new LiteDatabase(_connectionString);

                return db.GetCollection<PayloadItem>().Find(queryExpression).ToList();
            });

        public override Task<bool> WritePacketAsync(PayloadItem packet) => 
            Task.Run(() =>
            {
                if (packet is null)
                {
                    Log.Error($"LiteDBDAL::WritePacketAsync - packet was null");

                    throw new ArgumentNullException(nameof(packet));
                }

                using var db = new LiteDatabase(_connectionString);

                return db.GetCollection<PayloadItem>().Insert(packet) > 0;
            });

        public override bool Initialize()
        {
            try
            {
                _connectionString = $"Filename={settingsItem.DAL_FileName}";

                using var db = new LiteDatabase(_connectionString);

                return true;
            } catch (Exception ex)
            {
                Log.Error($"LiteDBDAL::Initialize - Exception when loading: {ex}");

                return false;
            }
        }
    }
}