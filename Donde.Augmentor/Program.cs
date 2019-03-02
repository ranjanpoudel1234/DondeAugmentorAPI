using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.Linq;

namespace Donde.Augmentor.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuringFileName = "nlog.config";
            // NLog: setup the logger first to catch all errors
            var logger = NLogBuilder.ConfigureNLog(configuringFileName).GetCurrentClassLogger();      
            try
            {
                logger.Debug("Application started");
                SetElasticBeanstalkConfiguration(logger);
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

        /// <summary>
        /// Load the environment variables from elastic beanstalk(including environment name itself before the webhostbuilder builds the context)
        /// </summary>
        /// <param name="logger"></param>
        private static void SetElasticBeanstalkConfiguration(Logger logger)
        {
            var ebConfigBuilder = new ConfigurationBuilder();

            ebConfigBuilder.AddJsonFile(
                @"C:\Program Files\Amazon\ElasticBeanstalk\config\containerconfiguration",
                optional: true,
                reloadOnChange: true
            );

            var configuration = ebConfigBuilder.Build();

            var elasticBeanstalkConfigKeyPairs =
                configuration.GetSection("iis:env")
                    .GetChildren()
                    .Select(pair => pair.Value.Split(new[] { '=' }, 2))
                    .ToDictionary(keypair => keypair[0], keypair => keypair[1]);

            foreach (var keyVal in elasticBeanstalkConfigKeyPairs)
            {
                Environment.SetEnvironmentVariable(keyVal.Key, keyVal.Value);
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
                    .AddEnvironmentVariables();
            })
            .CaptureStartupErrors(true)
            .UseSetting("detailedErrors", "true")
            .UseStartup<Startup>()     
            .ConfigureLogging(logging =>
            {
                //this is important to set, because MS sets it to information when you add custom logging on top of it.
                //we allow this here and override by using nlog.config level.
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            })
            .Build();
        }    
    }
}
