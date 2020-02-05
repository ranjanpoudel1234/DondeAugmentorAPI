using Amazon.S3;
using Donde.Augmentor.Bootstrapper;
using Donde.Augmentor.Core.Domain;
using Donde.Augmentor.Web.AwsEnvironmentConfiguration;
using Donde.Augmentor.Web.Cors;
using Donde.Augmentor.Web.Filters;
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
using NLog;
using NLog.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using System.Reflection;
using Donde.Augmentor.Core.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Donde.Augmentor.Infrastructure.Database.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
        private AppSetting AppSettings { get; set; }
        private DomainSettings DomainSettings { get; set; }
        private bool IsLocalEnvironment => CurrentEnvironment.EnvironmentName.Equals("Local") || CurrentEnvironment.EnvironmentName.Equals("Vagrant");

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            IntegrateSimpleInjector(services);
            services.AddOptions();

            AppSettings = Configuration.GetSection("Donde.Augmentor.Settings").Get<AppSetting>();
            DomainSettings = Configuration.GetSection("Donde.Augmentor.DomainSettings").Get<DomainSettings>();

            services.ConfigureCorsPolicy(AppSettings.Host.CorsPolicy);

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddDondeOData(Configuration);

            services.AddMvc(
               config =>
               {
                   config.Filters.Add(typeof(DondeCustomExceptionFilter));
               });

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();


            //necessary here otherwise the Account controller will give registration issue on userStore
            services.AddDbContext<DondeIdentityContext>(options =>
             options.UseNpgsql(GetConnectionString())
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning)));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DondeIdentityContext>();

            //identityServer code
            var builder = services.AddIdentityServer()
              .AddInMemoryIdentityResources(Config.GetIdentityResources())
              .AddInMemoryApiResources(Config.GetApiResources())
              .AddInMemoryClients(Config.GetClients(Configuration.GetValue("AppSettings:Address", "")))
              .AddAspNetIdentity<User>();

            if (IsLocalEnvironment)
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }
     
            services.ConfigureApplicationCookie((obj) =>
            {
                obj.LoginPath = "/Accounts/Login";
                obj.LogoutPath = "/Accounts/Logout";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env,
            VersionedODataModelBuilder modelBuilder,
            ILoggerFactory loggerFactory)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseMvc();

            app.UseDondeOData();

            InitializeAndVerifyContainer(app, loggerFactory);

            AddNLog(loggerFactory);

            app.UseDondeCorsPolicy();
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

            container.Register(() => DomainSettings);
           
            DondeAugmentorBootstrapper.BootstrapDondeAugmentor
                (container, 
                Assembly.GetExecutingAssembly(),
                GetConnectionString(), 
                CurrentEnvironment.EnvironmentName,
                loggerFactory);
          
            // Allow Simple Injector to resolve services from ASP.NET Core.
            container.AutoCrossWireAspNetComponents(app);

            container.Verify();
        }

        private string GetConnectionString()
        {
            var connectionString = string.Empty;
            if (IsLocalEnvironment)
            {
                connectionString = Configuration["Donde.Augmentor.Data:API:ConnectionString"];
            }
            else
            {
                connectionString = Configuration.GetRdsConnectionString();
            }

            return connectionString;
        }
     
        private void AddNLog(ILoggerFactory loggerFactory)
        {
            var connectionString = GetConnectionString();

            GlobalDiagnosticsContext.Set("connectionString", connectionString);
            LogManager.Configuration.Variables["dondeAugmentorNlogConnectionstring"] = connectionString; // key matches one in nlog.config file

            ReconfigureNLogRulesBasedOnEnvironment();

            loggerFactory.AddNLog();

            loggerFactory.CreateLogger<Program>().LogInformation($"Donde_Augmentor: ConnectionString: {connectionString} and EnvironmentName: {CurrentEnvironment.EnvironmentName}");
        }

        public void ReconfigureNLogRulesBasedOnEnvironment()
        {
            var logLevelsToDisable = Configuration.GetLogLevelsToDisable();
            if (!IsLocalEnvironment)
            {
                foreach (var rule in LogManager.Configuration.LoggingRules)
                {
                    logLevelsToDisable.ForEach(level => rule.DisableLoggingForLevel(level));
                }
            }
            LogManager.ReconfigExistingLoggers();
        }
    }
}
