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
        private string _connectionString;

        public LiteDBDAL(SettingsItem settings)
        {
            _connectionString = settings.DAL_FileName;
        }

        public override Task<List<PayloadItem>> GetHostPacketsAsync(string hostName) => 
            Task.Run(() =>
            {
                using var db = new LiteDatabase(_connectionString);

                return db.GetCollection<PayloadItem>().Find(a => a.HostName == hostName).ToList();
            });

        public override Task<List<PayloadItem>> QueryPacketsAsync(Expression<Func<PayloadItem, bool>> queryExpression) => 
            Task.Run(() =>
            {
                using var db = new LiteDatabase(_connectionString);

                return db.GetCollection<PayloadItem>().Find(queryExpression).ToList();
            });

        public override Task<bool> WritePacketAsync(PayloadItem packet) => 
            Task.Run(() =>
            {
                using var db = new LiteDatabase(_connectionString);

                return db.GetCollection<PayloadItem>().Insert(packet) > 0;
            });
    }
}