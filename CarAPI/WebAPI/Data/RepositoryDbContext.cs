using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class RepositoryDbContext : IdentityDbContext<ApplicationUser>
    {
        public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Brand>? Brands { get; set; }
        public DbSet<Model>? Models { get; set; }
        public DbSet<Feature>? Features { get; set; }
        public DbSet<ModelFeature>? ModelFeatures { get; set; }
        public DbSet<Car>? Cars { get; set; }
        public DbSet<CarFeature>? CarFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure unique constraint for BrandName
            builder.Entity<Brand>()
                .HasIndex(b => b.Name)
                .IsUnique();

            // Configure unique constraint for FeatureName
            builder.Entity<Feature>()
                .HasIndex(f => f.Name)
                .IsUnique();

            // Configure many-to-many relationship between Model and Feature via ModelFeature
            builder.Entity<ModelFeature>()
                .HasKey(mf => new { mf.ModelId, mf.FeatureId });

            builder.Entity<ModelFeature>()
                .HasOne(mf => mf.Model)
                .WithMany(m => m.ModelFeatures)
                .HasForeignKey(mf => mf.ModelId);
                

            builder.Entity<ModelFeature>()
                .HasOne(mf => mf.Feature)
                .WithMany(f => f.ModelFeatures)
                .HasForeignKey(mf => mf.FeatureId);

            // Configure many-to-many relationship between Car and Feature via CarFeature
            builder.Entity<CarFeature>()
                .HasKey(cf => new { cf.CarId, cf.FeatureId });

            builder.Entity<CarFeature>()
                .HasOne(cf => cf.Car)
                .WithMany(c => c.CarFeatures)
                .HasForeignKey(cf => cf.CarId);

            builder.Entity<CarFeature>()
                .HasOne(cf => cf.Feature)
                .WithMany()
                .HasForeignKey(cf => cf.FeatureId);

            // Configure relationships to avoid multiple cascade paths
            builder.Entity<Car>()
                .HasOne(c => c.Model)
                .WithMany()
                .HasForeignKey(c => c.ModelId)
                .OnDelete(DeleteBehavior.Restrict);  // Disable cascade delete for Model

            builder.Entity<Car>()
                .HasOne(c => c.Brand)
                .WithMany()
                .HasForeignKey(c => c.BrandId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
