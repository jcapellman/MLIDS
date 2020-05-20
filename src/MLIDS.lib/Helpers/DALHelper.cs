using System;
using System.Collections.Generic;
using System.Linq;

using MLIDS.lib.DAL.Base;

namespace MLIDS.lib.Helpers
{
    public static class DALHelper
    {
        public static List<BaseDAL> GetAvailableDALs() => 
            typeof(DALHelper).Assembly.GetTypes().Where(a => typeof(BaseDAL) == 
            a.BaseType && !a.IsAbstract).Select(b => (BaseDAL)Activator.CreateInstance(b)).ToList();
    }
}