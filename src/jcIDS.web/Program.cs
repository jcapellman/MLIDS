using System;
using System.IO;

using jcIDS.web.Managers;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using NLog.Web;

namespace jcIDS.web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            if (!File.Exists(Common.Constants.FILENAME_CONFIGURATION))
            {
                ConfigurationManager.WriteDefaultConfiguration(Path.Combine(AppContext.BaseDirectory, Common.Constants.FILENAME_CONFIGURATION));
            }

            var config = new ConfigurationBuilder()
                .AddJsonFile(Common.Constants.FILENAME_CONFIGURATION)
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();
        }
    }
}