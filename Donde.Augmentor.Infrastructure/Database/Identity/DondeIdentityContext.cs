using Donde.Augmentor.Core.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Donde.Augmentor.Infrastructure.Database.Identity
{
    public class DondeIdentityContext : IdentityDbContext<User>
    {
        public DondeIdentityContext(DbContextOptions<DondeIdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-3.0
        }
    }
}
