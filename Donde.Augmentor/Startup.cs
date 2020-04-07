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
using Microsoft.EntityFrameworkCore;
using System;
using Donde.Augmentor.Infrastructure.Database.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Donde.Augmentor.Web.Identity;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;

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
        private SignInKeyCredentialSettings IdentitySignInKeyCredentialSettings { get; set; }
        private Client[] Clients { get; set; }
        private bool IsLocalEnvironment => CurrentEnvironment.EnvironmentName.Equals("Local") || CurrentEnvironment.EnvironmentName.Equals("Vagrant");

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            AppSettings = Configuration.GetSection("Donde.Augmentor.Settings").Get<AppSetting>();
            DomainSettings = Configuration.GetSection("Donde.Augmentor.DomainSettings").Get<DomainSettings>();
            Clients = Configuration.GetSection("Donde.Augmentor.IdentitySettings:Clients").Get<Client[]>();
            IdentitySignInKeyCredentialSettings = Configuration.GetSection("Donde.Augmentor.IdentitySettings:SigninKeyCredentials").Get<SignInKeyCredentialSettings>();

            //necessary here otherwise the Account controller will give registration issue on userStore
             services.AddDbContext<DondeIdentityContext>(options =>
             options.UseNpgsql(GetConnectionString())
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning)));

            services.AddDondeIdentityServer(IsLocalEnvironment, Clients, IdentitySignInKeyCredentialSettings);
           

            services.AddAuthorization();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme; // straight 401
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // causes 401 instead of 404 and redirect
            })
            .AddIdentityServerAuthentication(options =>
            {              
                options.Authority = AppSettings.Host.APIEndPointUrl;
                options.RequireHttpsMetadata = false;
                options.ApiName = "donde-api";
            });

            IntegrateSimpleInjector(services);

            services.AddOptions();

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
