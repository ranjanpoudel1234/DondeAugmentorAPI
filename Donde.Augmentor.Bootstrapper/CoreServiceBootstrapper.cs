using Amazon.S3;
using Donde.Augmentor.Core.Services.Services;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Donde.Augmentor.Bootstrapper
{
    public class CoreServiceBootstrapper : BaseBootstrapper
    {
        public static void BootstrapCoreService(Container simpleInjectorContainer)
        {
            RegisterInstancesByNamespace(simpleInjectorContainer,
                 new List<Assembly>
                {
                    GetServiceAssembly()
                },
                new List<string>
                {
                    "Donde.Augmentor.Core.Services.Services"
                });

           // simpleInjectorContainer.Register(() => IAmazonS3, LifeStyle.Scoped);
            //any fluent validators can be registered here.
        }

        protected static Func<Assembly> GetServiceInterfaceAssembly { get; } = () => Assembly.Load("Donde.Augmentor.Core.Service.Interfaces");
        protected static Func<Assembly> GetServiceAssembly { get; } = () => Assembly.Load("Donde.Augmentor.Core.Services");
    }
}
