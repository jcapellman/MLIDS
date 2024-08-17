using MLIDS.lib.Containers;
using MLIDS.lib.DAL.Base;

namespace MLIDS.lib.Models.Base
{
    public abstract class BaseModelRunner
    {
        public abstract string ModelTypeName { get; }

        protected abstract bool Run(string modelFile, BaseDal dataLayer, SettingsItem settingsItem);
    }
}