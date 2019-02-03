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
        public static void AddAionOData
            (this IServiceCollection services, 
            IConfigurationRoot configurationRoot,
            string configurationKey = null)
        {
            services.AddOData().EnableApiVersioning();

            var modelConfigurationContainingAssemblies = new List<Assembly> { Assembly.GetEntryAssembly() };

            var modelConfigurationsTypes = GetClassesImplementing<IModelConfiguration>(modelConfigurationContainingAssemblies);

            foreach (var modelConfigurationType in modelConfigurationsTypes)
                services.TryAddEnumerable(Transient(typeof(IModelConfiguration), modelConfigurationType));
        }

        private static ServiceDescriptor Transient(object @interface, object implementation)
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<Type> GetClassesImplementing<TInterface>(List<Assembly> assemblies)
        {
            var type = typeof(TInterface);
            return assemblies.SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));
        }
    }
}
