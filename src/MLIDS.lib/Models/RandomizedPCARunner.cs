﻿using System;

using MLIDS.lib.Containers;
using MLIDS.lib.DAL.Base;
using MLIDS.lib.Models.Base;

namespace MLIDS.lib.Models
{
    public class RandomizedPCARunner : BaseModelRunner
    {
        public override string ModelTypeName => "Randomized PCA";

        protected override bool Run(string modelFile, BaseDAL dataLayer, SettingsItem settingsItem)
        {
            throw new NotImplementedException();
        }
    }
}