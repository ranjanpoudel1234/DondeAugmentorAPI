using Donde.Augmentor.Core.Domain.Validations;
using FluentValidation;
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
                    "Donde.Augmentor.Core.Services.Services",
                    "Donde.Augmentor.Core.Services.Services.FileService"
                });

            //any fluent validators can be registered here.
            simpleInjectorContainer.Register(typeof(IValidator<>), typeof(MediaAttachmentDtoValidator).Assembly, Lifestyle.Scoped);
        }

        protected static Func<Assembly> GetServiceInterfaceAssembly { get; } = () => Assembly.Load("Donde.Augmentor.Core.Service.Interfaces");
        protected static Func<Assembly> GetServiceAssembly { get; } = () => Assembly.Load("Donde.Augmentor.Core.Services");
    }
}
