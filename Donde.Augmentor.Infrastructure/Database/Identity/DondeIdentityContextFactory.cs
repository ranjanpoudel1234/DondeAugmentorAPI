using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Reflection;

namespace Donde.Augmentor.Infrastructure.Database.Identity
{
    /// <summary>
    /// Since our DbContext lives in a class library project, we need to give Entity Framework Core's Tools a little help in creating it before we can
    /// use any of the CLI commands to create and apply a migration. 
    /// This is done by implementing the IDesignTimeDbContextFactory<TContext> interface within a class in the same project as the DbContext(s).
    /// </summary>
    class DondeIdentityContextFactory : IDesignTimeDbContextFactory<DondeContext>
    {
        public DondeContext CreateDbContext(string[] args)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<DondeContext>();
            dbContextOptionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=Donde_Augmentor;Username=donde_postgress;Password=D0ND3p0stgr3s",
                npgSqlBuilder => npgSqlBuilder.MigrationsAssembly(GetInfrastructureAssembly().FullName));

                return new DondeContext(dbContextOptionsBuilder.Options);
        }

        protected static Func<Assembly> GetInfrastructureAssembly { get; } = () => Assembly.Load("Donde.Augmentor.Infrastructure");
    }
}
