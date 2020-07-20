using Amazon.S3;
using Donde.Augmentor.Bootstrapper;
using Donde.Augmentor.Core.Domain;
using Donde.Augmentor.Infrastructure.Database.Identity;
using Donde.Augmentor.Web.AwsEnvironmentConfiguration;
using Donde.Augmentor.Web.Cors;
using Donde.Augmentor.Web.Filters;
using Donde.Augmentor.Web.Identity;
using Donde.Augmentor.Web.OData;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            services.AddMvc(
               config =>
               {
                   config.Filters.Add(typeof(DondeCustomExceptionFilter));

                   foreach (var outputFormatter in
config.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ =>
_.SupportedMediaTypes.Count == 0))
                   {
                       outputFormatter.SupportedMediaTypes.Add(new
   MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                   }
                   foreach (var inputFormatter in
   config.InputFormatters.OfType<ODataInputFormatter>().Where(_ =>
   _.SupportedMediaTypes.Count == 0))
                   {
                       inputFormatter.SupportedMediaTypes.Add(new
   MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                   }
               }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();

            services.AddDondeOData(Configuration);

            //services.AddSwaggerGen(a =>
            //{
            //    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

            //    foreach (var description in provider.ApiVersionDescriptions)
            //    {
            //        var info = new Microsoft.OpenApi.Models.OpenApiInfo
            //        {
            //            Title = "My API Title",
            //            Version = description.ApiVersion.ToString(),
            //            Description = "My API Description"
            //        };

            //        if (description.IsDeprecated)
            //            info.Description += " NOTE: This API has been deprecated";

            //        a.SwaggerDoc(description.GroupName, info);
            //    }

            //    //a.ParameterFilter<SwaggerDefaultValues>();

            //    var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, $"{PlatformServices.Default.Application.ApplicationName}.xml");
            //    a.IncludeXmlComments(filePath);

            //    a.DescribeAllEnumsAsStrings();
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Swagger Sample",
                    Version = "v1",
                    // You can also set Description, Contact, License, TOS...
                });

             
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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            //app.UseSwaggerUI(c =>
            //{
            //    // build a swagger endpoint for each discovered API version
            //    foreach (var description in versionDescriptionProvider.ApiVersionDescriptions)
            //    {
            //        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
            //    }
            //});
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Sample");
            });

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

        private void ReconfigureNLogRulesBasedOnEnvironment()
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

        private static void SetOutputFormatters(IServiceCollection services)
        {
            services.AddMvcCore(options =>
            {
                IEnumerable<ODataOutputFormatter> outputFormatters =
                    options.OutputFormatters.OfType<ODataOutputFormatter>()
                        .Where(foramtter => foramtter.SupportedMediaTypes.Count == 0);

                foreach (var outputFormatter in outputFormatters)
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/odata"));
                }
            });
        }
    }
}
