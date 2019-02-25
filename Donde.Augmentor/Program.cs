using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Donde.Augmentor.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Vagrant");
            CreateWebHostBuilder(args).Run();           
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
            .Build();
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
