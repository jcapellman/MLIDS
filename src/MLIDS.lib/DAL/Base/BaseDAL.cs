using MLIDS.lib.ML.Objects;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MLIDS.lib.DAL.Base
{
    public abstract class BaseDAL
    {
        public abstract string Description { get; }

        public abstract Task<bool> WritePacketAsync(PayloadItem packet);

        public abstract Task<List<PayloadItem>> GetHostPacketsAsync(string hostName);

        public abstract Task<List<PayloadItem>> QueryPacketsAsync(System.Linq.Expressions.Expression<Func<PayloadItem, bool>> queryExpression);
    }
}