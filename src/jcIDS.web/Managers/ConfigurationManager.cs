using System;
using System.IO;
using System.Linq;

using jcIDS.lib.CommonObjects;
using jcIDS.web.Objects;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

namespace jcIDS.web.Managers
{
    public class ConfigurationManager
    {
        public static ReturnSet<ConfigurationValues> ParseConfiguration(IConfiguration configuration)
        {
            try
            {
                var config = new ConfigurationValues();

                var properties = typeof(ConfigurationValues).GetProperties().ToList();

                foreach (var property in properties)
                {
                    var propertyValue = configuration[property.Name];

                    property.SetValue(config, propertyValue);
                }

                return new ReturnSet<ConfigurationValues>(config);
            }
            catch (Exception ex)
            {
                return new ReturnSet<ConfigurationValues>(ex, "Failed to parse configuration file");
            }
        }

        public static void WriteDefaultConfiguration(string fileName)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(new ConfigurationValues()));
        }
    }
}