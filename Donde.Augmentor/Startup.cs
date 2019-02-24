using Donde.Augmentor.Bootstrapper;
using Donde.Augmentor.Web.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using System.Collections.Generic;
using System.Reflection;

namespace Donde.Augmentor.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            Configuration = config as IConfigurationRoot;        
            CurrentEnvironment = env;
        }

        public IConfigurationRoot Configuration { get; }
        private IHostingEnvironment CurrentEnvironment { get; }
        private Container container = new Container();

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            IntegrateSimpleInjector(services);
            services.AddOptions();

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddDondeOData(Configuration);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env,
            VersionedODataModelBuilder modelBuilder,
            ILoggerFactory loggerFactory)
        {
            SetupAWSLogger(loggerFactory);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseDondeOData();

            InitializeAndVerifyContainer(app, loggerFactory);
        }
     
        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(container));

            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        private void InitializeAndVerifyContainer(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            // Add application presentation components:
             container.RegisterMvcControllers(app);

            //var connectionString = Configuration["Donde.Augmentor.Data:API:ConnectionString"];
            var connectionString = "Server=dondedbinstance.cgkhjsd3ndot.us-east-1.rds.amazonaws.com;Port=5432;Database=Donde_Augmentor;Username=postgresDondeDev;Password=postgresDondeDev";
            // Example Logging
            loggerFactory.CreateLogger<Program>().LogInformation($"Donde_Augmentor: ConnectionString: {connectionString} and EnvironmentName: {CurrentEnvironment.EnvironmentName}");
            DondeAugmentorBootstrapper.BootstrapDondeAugmentor
                (container, 
                Assembly.GetExecutingAssembly(),
                connectionString, 
                CurrentEnvironment.EnvironmentName,
                loggerFactory);
          
            // Allow Simple Injector to resolve services from ASP.NET Core.
            container.AutoCrossWireAspNetComponents(app);

            container.Verify();
        }

        private void SetupAWSLogger(ILoggerFactory loggerFactory)
        {
            // Create a logging provider based on the configuration information passed through the appsettings.json
            loggerFactory.AddAWSProvider(Configuration.GetAWSLoggingConfigSection());
            // Create a logger instance from the loggerFactory
            var logger = loggerFactory.CreateLogger<Program>();

            // Example Logging
            logger.LogInformation("Example DondeAugmentor logging that logs to AWS Cloudwatch");        
        }

        private string GetRdsConnectionString()
        {
            string hostname = Configuration.GetValue<string>("RDS_HOSTNAME");
            string port = Configuration.GetValue<string>("RDS_PORT");
            string dbname = Configuration.GetValue<string>("RDS_DB_NAME");
            string username = Configuration.GetValue<string>("RDS_USERNAME");
            string password = Configuration.GetValue<string>("RDS_PASSWORD");

            return $"Server={hostname};Port={port};Database={dbname};Username={username};Password={password}";
        }

    }
}
