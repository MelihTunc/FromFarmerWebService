using Microsoft.Extensions.Configuration;
using System;

namespace FromFarmer.Utilities.Helpers
{
    public class ConfigurationHelper
    {
        private static IConfigurationRoot configurationRoot;

        public ConfigurationHelper()
        {
            if (configurationRoot == null)
            {
                configurationRoot = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("connectionsettings.json", optional: true, reloadOnChange: true).Build();
            }
        }

        public static IConfiguration GetConfig()
        {
            GetConfigFileSet();
            return configurationRoot;
        }

        public static void GetConfigFileSet()
        {
            if (configurationRoot == null)
            {
                configurationRoot = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("connectionsettings.json", optional: true, reloadOnChange: true).Build();
            }
        }
    }
}
