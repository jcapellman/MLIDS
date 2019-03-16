using System.ComponentModel;

namespace jcIDS.web.Objects
{
    public class ConfigurationValues
    {
        public string DatabaseConnection { get; set; }

        [DefaultValue(true)]
        public bool AutoDeviceAdoption { get; set; }
    }
}