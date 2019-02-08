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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env,
            VersionedODataModelBuilder modelBuilder,
            ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseDondeOData();

            InitializeAndVerifyContainer(app, loggerFactory);
        }

        private void InitializeAndVerifyContainer(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            // Add application presentation components:
             container.RegisterMvcControllers(app);
            //container.RegisterMvcViewComponents(app);

            var connectionString = Configuration["Donde.Augmentor.Data:API:ConnectionString"];
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
    }
}
