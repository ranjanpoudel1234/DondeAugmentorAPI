using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Donde.Augmentor.Bootstrapper
{
    public class DondeAugmentorBootstrapper : BaseBootstrapper
    {
        public static void BootstrapDondeAugmentor
            (Container simpleInjectorContainer, 
            Assembly webAssembly, 
            string connectionString,
            string environmentName,
            ILoggerFactory loggerFactory)
        {
            BootstrapAutoMapper(simpleInjectorContainer, webAssembly);
            CoreServiceBootstrapper.BootstrapCoreService(simpleInjectorContainer);
            InfrastructureBootstrapper.BootstrapInfrastructure(simpleInjectorContainer, connectionString, environmentName, loggerFactory);
        }

        private static void BootstrapAutoMapper(Container simpleInjectorContainer, Assembly webAssembly)
        {
            var assembliesToBootstrapFrom = new List<Assembly>
            {
                webAssembly,
                Assembly.Load("Donde.Augmentor.Core.Services")
            };

            var profiles = assembliesToBootstrapFrom.SelectMany(x => x.GetTypes())
                                                    .Where(x => typeof(AutoMapper.Profile).IsAssignableFrom(x));

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(Activator.CreateInstance(profile) as AutoMapper.Profile);
                }
            });

            simpleInjectorContainer.Register(() => config.CreateMapper(simpleInjectorContainer.GetInstance), Lifestyle.Scoped);
        }
    }
}
