using AutoMapper;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Donde.Augmentor.Bootstrapper
{
    public class BaseBootstrapper
    {
        protected static void RegisterInstancesByNamespace
            (Container simpleInjectorContainer,
            List<Assembly> assemblies,
            List<string> namespaces)
        {
            var registrations =
           from type in assemblies.SelectMany(a => a.GetExportedTypes())
           where namespaces.Contains(type.Namespace)
           where type.GetInterfaces().Any()
           select new { Service = type.GetInterfaces().Single(), Implementation = type };

            foreach (var reg in registrations)
            {
                simpleInjectorContainer.Register(reg.Service, reg.Implementation);
            }
        }

        protected static void RegisterInfrastructureInstancesByNamespace
           (Container simpleInjectorContainer,
           List<Assembly> assemblies,
           List<string> namespaces)
        {
            var registrations =
           from type in assemblies.SelectMany(a => a.GetExportedTypes())
           where namespaces.Contains(type.Namespace)
           where type.GetInterfaces().Any()
           select new { Service = type.GetInterfaces().SingleOrDefault(x => x.Name.Contains(type.Name)), Implementation = type };

            foreach (var reg in registrations)
            {
                simpleInjectorContainer.Register(reg.Service, reg.Implementation);
            }
        }
    }
}
