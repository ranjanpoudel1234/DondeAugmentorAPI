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
                    GetServiceInterfaceAssembly(),
                    GetServiceAssembly()
                },
                new List<string>
                {
                    "Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces",
                    "Donde.Augmentor.Core.Services.Services"
                });

            //any fluent validators can be registered here.
        }

        protected static Func<Assembly> GetServiceInterfaceAssembly { get; } = () => Assembly.Load("Donde.Augmentor.Core.Services.Interfaces");
        protected static Func<Assembly> GetServiceAssembly { get; } = () => Assembly.Load("Donde.Augmentor.Core.Services");
    }
}
