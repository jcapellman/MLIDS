using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;

using MLIDS.lib.Common;
using MLIDS.lib.Containers;
using MLIDS.lib.DAL.Base;
using MLIDS.lib.ML.Objects;

namespace MLIDS.lib.DAL
{
    public class JsonFileDAL : BaseDAL
    {
        private string _fileName;

        public JsonFileDAL(string fileName = Constants.DAL_FileName)
        {
            _fileName = fileName;
        }

        public JsonFileDAL(SettingsItem settings) : this(settings.DAL_FileName) { }

        public override async Task<List<PayloadItem>> GetHostPacketsAsync(string hostName)
        {
            using var fs = File.OpenRead(_fileName);

            var data = await JsonSerializer.DeserializeAsync<List<PayloadItem>>(fs);

            return data.Where(a => a.HostName == hostName).ToList();
        }
        
        public override async Task<List<PayloadItem>> QueryPacketsAsync(Expression<Func<PayloadItem, bool>> queryExpression)
        {
            using var fs = File.OpenRead(_fileName);

            var data = await JsonSerializer.DeserializeAsync<List<PayloadItem>>(fs);

            return data.AsQueryable().Where(queryExpression).ToList();
        }

        public override async Task<bool> WritePacketAsync(PayloadItem packet)
        {
            using var fs = File.OpenRead(_fileName);

            var data = await JsonSerializer.DeserializeAsync<List<PayloadItem>>(fs);

            data.Add(packet);

            await File.WriteAllTextAsync(_fileName, JsonSerializer.Serialize(data));

            return true;
        }
    }
}