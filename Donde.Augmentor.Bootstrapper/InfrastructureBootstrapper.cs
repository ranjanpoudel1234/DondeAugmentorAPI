using Donde.Augmentor.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Donde.Augmentor.Bootstrapper
{
    public class InfrastructureBootstrapper : BaseBootstrapper
    {
        public static void BootstrapInfrastructure(Container simpleInjectorContainer, string connectionString,
            string environmentName, ILoggerFactory loggerFactory)
        {
            RegisterInfrastructureInstancesByNamespace(simpleInjectorContainer,
                new List<Assembly>
                {
                   // GetInfrastructureInterfaceAssembly(),
                    GetInfrastructureAssembly()
                },
                 new List<string>
                 {
                   // "Donde.Augmentor.Core.Repositories.Interfaces.RepositoryInterfaces",
                    "Donde.Augmentor.Infrastructure.Repositories"
                 }
            );

            var options = BuildDondeContextOptions(environmentName, connectionString, loggerFactory);
            simpleInjectorContainer.Register(() => { return new DondeContext(options.Options); }, Lifestyle.Scoped);
        }

        private static DbContextOptionsBuilder<DondeContext> BuildDondeContextOptions(string environmentName, string connectionString, ILoggerFactory loggerFactory)
        {
            var dbContextOptions = new DbContextOptionsBuilder<DondeContext>();
            
            if(environmentName.Equals("Development"))
            {
                dbContextOptions.UseNpgsql(connectionString, npgSqlBuilder => npgSqlBuilder.MigrationsAssembly(GetInfrastructureAssembly().FullName))
                    .UseLoggerFactory(loggerFactory)
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            }
            else
            {
                dbContextOptions.UseNpgsql(connectionString, npgSqlBuilder => npgSqlBuilder.MigrationsAssembly(GetInfrastructureAssembly().FullName));
            }

            var dondeContext = new DondeContext(dbContextOptions.Options);
            dondeContext.Database.Migrate();

            // get the data from databuilder and add to the context/database
            return dbContextOptions;
        }

        protected static Func<Assembly> GetInfrastructureInterfaceAssembly { get; } = () => Assembly.Load("Donde.Augmentor.Core.Repositories.Interfaces");
        protected static Func<Assembly> GetInfrastructureAssembly { get; } = () => Assembly.Load("Donde.Augmentor.Infrastructure");
    }
}
