using System;
using System.Collections.Generic;
using System.Linq;

using MLIDS.lib.Containers;
using MLIDS.lib.DAL.Base;

namespace MLIDS.lib.Helpers
{
    public static class DALHelper
    {
        public static List<BaseDAL> GetAvailableDALs(SettingsItem settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return typeof(DALHelper).Assembly.GetTypes().Where(a => typeof(BaseDAL) ==
            a.BaseType && !a.IsAbstract).Select(b =>
            (BaseDAL)Activator.CreateInstance(b, new[] { settings })).OrderByDescending(c =>
            !c.IsSelectable).ThenBy(d => d.Description).ToList();
        }             
    }
}