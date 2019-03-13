using System.Linq;

using jcIDS.web.Objects;

using Microsoft.Extensions.Configuration;

namespace jcIDS.web.Managers
{
    public class SettingsManager
    {
        public static ConfigurationValues ParseConfiguration(IConfiguration configuration)
        {
            var config = new ConfigurationValues();

            var properties = typeof(ConfigurationValues).GetProperties().ToList();

            foreach (var property in properties)
            {
                var propertyValue = configuration[property.Name];

                property.SetValue(config, propertyValue);
            }

            return config;
        }
    }
}