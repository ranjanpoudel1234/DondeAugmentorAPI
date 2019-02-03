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
        public static void BootstrapInfrastructure(Container simpleInjectorContainer)
        {
            RegisterInstancesByNamespace(simpleInjectorContainer,
                new List<Assembly>
                {
                    GetInfrastructureInterfaceAssembly(),
                    GetInfrastructureAssembly()
                },
                 new List<string>
                 {
                    "Donde.Augmentor.Core.Service.Interfaces.ServiceInterfaces",
                    "Donde.Augmentor.Core.Services.Services"
                 }
            );
        }

        private static DbContextOptionsBuilder<DondeContext> BuildDondeContextOptions(string environmentName, string connectionString, ILoggerFactory loggerFactory)
        {
            var dbContextOptions = new DbContextOptionsBuilder<DondeContext>();
            
            if(environmentName.Equals("vagrant"))
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

            return dbContextOptions;
        }

        protected static Func<Assembly> GetInfrastructureInterfaceAssembly { get; } = () => Assembly.Load("Donde.Augmentor.Core.Repositories.Interfaces");
        protected static Func<Assembly> GetInfrastructureAssembly { get; } = () => Assembly.Load("Donde.Augmentor.Infrastructure");
    }
}
