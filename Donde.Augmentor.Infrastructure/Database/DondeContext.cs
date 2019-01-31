using Donde.Augmentor.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Donde.Augmentor.Infrastructure.Database
{
    public class DondeContext : DbContext
    {
        public DondeContext()
        {

        }

        public DondeContext(DbContextOptions<DondeContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Audio> Audios { get; set; }
        public DbSet<AugmentImage> AugmentImages { get; set; }
        public DbSet<AugmentObject> AugmentObjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyIndexes(modelBuilder);
        }

        private void ApplyIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Id)
                .IsUnique();

            modelBuilder.Entity<Organization>()
                .HasIndex(x => x.Id)
                .IsUnique();

            modelBuilder.Entity<Avatar>()
               .HasIndex(x => x.Id)
               .IsUnique();

            modelBuilder.Entity<Audio>()
               .HasIndex(x => x.Id)
               .IsUnique();

            modelBuilder.Entity<AugmentImage>()
               .HasIndex(x => x.Id)
               .IsUnique();

            modelBuilder.Entity<AugmentObject>()
               .HasIndex(x => x.Id)
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
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=Donde_Augmentor;Username=postgres;Password=postgres");
        }
    }
}
