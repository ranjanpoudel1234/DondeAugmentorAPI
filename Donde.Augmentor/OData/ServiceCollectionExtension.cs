using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Donde.Augmentor.Web.OData
{
    public static class ServiceCollectionExtension
    {
        public static void AddDondeOData
            (this IServiceCollection services, 
            IConfigurationRoot configurationRoot,
            string configurationKey = null)
        {
            services.AddOData().EnableApiVersioning();

            var modelConfigurationContainingAssemblies = new List<Assembly> { Assembly.GetEntryAssembly() };

            var modelConfigurationsTypes = GetClassesImplementing<IModelConfiguration>(modelConfigurationContainingAssemblies);

            foreach (var modelConfigurationType in modelConfigurationsTypes)
                services.TryAddEnumerable(ServiceDescriptor.Transient(typeof(IModelConfiguration), modelConfigurationType));
        }

        private static IEnumerable<Type> GetClassesImplementing<TInterface>(List<Assembly> assemblies)
        {
            var type = typeof(TInterface);
            return assemblies.SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));
        }
    }
}
