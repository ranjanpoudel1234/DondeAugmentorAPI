using Donde.Augmentor.Core.Domain.Interfaces;
using Donde.Augmentor.Core.Domain.Models;
using Donde.Augmentor.Core.Domain.Models.Identity;
using Donde.Augmentor.Core.Domain.Models.Metrics;
using Donde.Augmentor.Core.Domain.Models.RolesAndPermissions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Donde.Augmentor.Infrastructure.Database
{
    public class DondeContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
		public DondeContext()
		{

		}

		public DondeContext(DbContextOptions<DondeContext> options) : base(options)
		{

		}
		public DbSet<Organization> Organizations { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
		public DbSet<Audio> Audios { get; set; }
		public DbSet<Video> Videos { get; set; }
		public DbSet<AugmentImage> AugmentImages { get; set; }
		public DbSet<AugmentObject> AugmentObjects { get; set; }
		public DbSet<AugmentObjectMedia> AugmentObjectMedias { get; set; }
		public DbSet<AugmentObjectLocation> AugmentObjectLocations { get; set; }
        public DbSet<AugmentObjectVisitMetric> AugmentObjectVisitMetrics { get; set; }
        public DbSet<AugmentObjectMediaVisitMetric> AugmentObjectMediaVisitMetrics { get; set; }


        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserOrganization> UserOrganizations { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            base.OnModelCreating(modelBuilder);
            ApplyIndexes(modelBuilder);
            modelBuilder.ApplyGlobalFilters<IAuditFieldsModel>(model => !((IAuditFieldsModel)model).IsDeleted);

        }

        private void ApplyIndexes(ModelBuilder modelBuilder)
		{		
			modelBuilder.Entity<AugmentObjectMedia>()
	       .HasIndex(u => u.AugmentObjectId)
	       .IsUnique();
		}

        /// <summary>
        ///This method allows the CLI to call to the context and get a provider configured. This is overriden for EF CLI.
        ///When using startup.cs and specifying a provider there,
        /// this value would not be used.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured) optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=Donde_Augmentor;Username=donde_postgress;Password=D0ND3p0stgr3s");
		}
	}
}
