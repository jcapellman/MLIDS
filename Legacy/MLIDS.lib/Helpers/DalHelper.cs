using System;
using System.Collections.Generic;
using System.Linq;

using MLIDS.lib.Containers;
using MLIDS.lib.DAL.Base;

namespace MLIDS.lib.Helpers
{
    public static class DalHelper
    {
        public static List<BaseDal> GetAvailableDALs(SettingsItem settings) => settings == null
                ? throw new ArgumentNullException(nameof(settings))
                : ([.. typeof(DalHelper).Assembly.GetTypes().Where(a => typeof(BaseDal) ==
                a.BaseType && !a.IsAbstract).Select(b =>
                (BaseDal)Activator.CreateInstance(b, settings)).OrderByDescending(c =>
                !c.IsSelectable).ThenBy(d => d.Description)]);
    }
}