using System;
using System.Collections.Generic;
using System.Linq;

using MLIDS.lib.Models.Base;

namespace MLIDS.lib.Helpers
{
    public static class ModelRunnerHelper
    {
        public static List<BaseModelRunner> GetAvailableRunners() =>
            typeof(BaseModelRunner).Assembly.GetTypes().Where(a => typeof(BaseModelRunner) ==
            a.BaseType && !a.IsAbstract).Select(b =>
            (BaseModelRunner)Activator.CreateInstance(b)).OrderBy(d => d.ModelTypeName).ToList();
    }
}