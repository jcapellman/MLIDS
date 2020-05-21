using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using LiteDB;

using MLIDS.lib.Common;
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

        public LiteDBDAL(string connectionString = Constants.DAL_FileName)
        {
            _connectionString = connectionString;
        }
            
        public LiteDBDAL(SettingsItem settings) : this(settings.DAL_FileName) { }
    
        public override Task<List<PayloadItem>> GetHostPacketsAsync(string hostName) => 
            Task.Run(() =>
            {
                using var db = new LiteDatabase(_connectionString);

                return db.GetCollection<PayloadItem>().Find(a => a.HostName == hostName).ToList();
            });

        public override Task<List<PayloadItem>> QueryPacketsAsync(Expression<Func<PayloadItem, bool>> queryExpression) => 
            Task.Run(() =>
            {
                if (queryExpression == null)
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
                if (packet == null)
                {
                    Log.Error($"LiteDBDAL::WritePacketAsync - packet was null");

                    throw new ArgumentNullException(nameof(packet));
                }

                using var db = new LiteDatabase(_connectionString);

                return db.GetCollection<PayloadItem>().Insert(packet) > 0;
            });
    }
}