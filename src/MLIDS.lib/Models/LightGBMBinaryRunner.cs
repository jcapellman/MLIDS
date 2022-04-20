using MLIDS.lib.Containers;
using MLIDS.lib.DAL.Base;
using MLIDS.lib.Models.Base;

namespace MLIDS.lib.Models
{
    public class LightGBMBinaryRunner : BaseModelRunner
    {
        public override string ModelTypeName => "LightGBM (Binary)";

        protected override bool Run(string modelFile, BaseDAL dataLayer, SettingsItem settingsItem)
        {
            throw new System.NotImplementedException();
        }
    }
}