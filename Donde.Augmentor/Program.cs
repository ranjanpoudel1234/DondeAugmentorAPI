using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;

namespace Donde.Augmentor.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuringFileName = "nlog.config";

            //If we inspect the Hosting aspnet code, we'll see that it internally looks the 
            //ASPNETCORE_ENVIRONMENT environment variable to determine the actual environment
            var aspnetEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var environmentSpecificLogFileName = $"nlog.{aspnetEnvironment}.config";

            if (File.Exists(environmentSpecificLogFileName))
            {
                configuringFileName = environmentSpecificLogFileName;
            }

            // NLog: setup the logger first to catch all errors
            var logger = NLogBuilder.ConfigureNLog(configuringFileName).GetCurrentClassLogger();
            try
            {
                logger.Debug("Application started");
                CreateWebHostBuilder(args).Run();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }                  
        }

        public static IWebHost CreateWebHostBuilder(string[] args)
        {
           return  WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.SetBasePath(context.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", false, true)
                    .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true)
                    .AddJsonFile(@"C:\Program Files\Amazon\ElasticBeanstalk\config\containerconfiguration", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .AddInMemoryCollection(GetAWSElasticBeanstalkConfiguration(builder));
            })
            .CaptureStartupErrors(true)
            .UseSetting("detailedErrors", "true")
            .UseStartup<Startup>()
            .ConfigureLogging(logging =>
            {
               // logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
            })
            .UseNLog()
            .Build();
        }

        private static void ConfigureNLog()
        {
          
        }

        private static Dictionary<string, string> GetAWSElasticBeanstalkConfiguration(IConfigurationBuilder builder)
        {
            IConfiguration configuration = builder.Build();
            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (IConfigurationSection pair in configuration.GetSection("iis:env").GetChildren())
            {
                string[] keypair = pair.Value.Split(new[] { '=' }, 2);
                dict.Add(keypair[0], keypair[1]);
            }       
            return dict;
        }
    }
}
