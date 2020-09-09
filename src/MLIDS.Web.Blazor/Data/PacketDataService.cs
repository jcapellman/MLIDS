using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MLIDS.lib.DAL.Base;
using MLIDS.lib.ML.Objects;

namespace MLIDS.Web.Blazor.Data
{
    public class PacketDataService
    {
        private BaseDAL _dal;

        public PacketDataService(BaseDAL dal)
        {
            _dal = dal;
        }

        public async Task<List<PayloadItem>> GetPayloadItemsAsync(DateTime startTime, DateTime endTime) => await _dal.QueryPacketsAsync(a => a.Timestamp > startTime && a.Timestamp < endTime);
    }
}