using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;

using MLIDS.lib.Containers;
using MLIDS.lib.DAL.Base;
using MLIDS.lib.ML.Objects;

namespace MLIDS.lib.DAL
{
    public class JsonFileDAL : BaseDAL
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        private const string DEFAULT_JSON_FILE = "db.json";

        private string _fileName;

        public override string Description => "JSON";

        public JsonFileDAL(SettingsItem settings) : base(settings) { }

        public override async Task<List<PayloadItem>> GetHostPacketsAsync(string hostName)
        {
            if (!File.Exists(_fileName))
            {
                return new List<PayloadItem>();
            }

            using var fs = File.OpenRead(_fileName);

            var data = await JsonSerializer.DeserializeAsync<List<PayloadItem>>(fs);

            return data.Where(a => a.HostName == hostName).ToList();
        }
        
        public override async Task<List<PayloadItem>> QueryPacketsAsync(Expression<Func<PayloadItem, bool>> queryExpression)
        {
            if (queryExpression == null)
            {
                Log.Error($"JsonFileDAL::QueryPacketsAsync - Query Expression was null");

                throw new ArgumentNullException(nameof(queryExpression));
            }

            if (!File.Exists(_fileName))
            {
                return new List<PayloadItem>();
            }

            using var fs = File.OpenRead(_fileName);

            var data = await JsonSerializer.DeserializeAsync<List<PayloadItem>>(fs);

            return data.AsQueryable().Where(queryExpression).ToList();
        }

        public override async Task<bool> WritePacketAsync(PayloadItem packet)
        {
            if (packet == null)
            {
                Log.Error($"JsonFileDAL::WritePacketAsync - packet was null");

                throw new ArgumentNullException(nameof(packet));
            }

            var data = new List<PayloadItem>();

            if (File.Exists(_fileName))
            {
                using var fs = File.OpenRead(_fileName);

                data = await JsonSerializer.DeserializeAsync<List<PayloadItem>>(fs);
            }

            data.Add(packet);

            await File.WriteAllTextAsync(_fileName, JsonSerializer.Serialize(data));

            return true;
        }

        public override bool Initialize()
        {
            _fileName = settingsItem.DAL_FileName ?? DEFAULT_JSON_FILE;

            return File.Exists(_fileName);
        }
    }
}