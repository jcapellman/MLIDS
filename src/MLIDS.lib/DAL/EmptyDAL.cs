using MLIDS.lib.Containers;
using MLIDS.lib.DAL.Base;
using MLIDS.lib.ML.Objects;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MLIDS.lib.DAL
{
    public class EmptyDAL : BaseDAL
    {
        public override string Description => "--Select a Data Layer--";

        public override bool IsSelectable => false;

        public EmptyDAL(SettingsItem settings) : base(settings) { }

        public override Task<List<PayloadItem>> GetHostPacketsAsync(string hostName)
        {
            throw new NotImplementedException();
        }

        public override bool Initialize() => false;

        public override Task<List<PayloadItem>> QueryPacketsAsync(Expression<Func<PayloadItem, bool>> queryExpression)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> WritePacketAsync(PayloadItem packet)
        {
            throw new NotImplementedException();
        }
    }
}