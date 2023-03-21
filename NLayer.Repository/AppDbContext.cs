using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Repository.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Foreign Key Many-to-Many
            modelBuilder.Entity<ProductTag>().HasKey(pt => new { pt.ProductId, pt.TagId });
            modelBuilder.Entity<ProductTag>()
                 .HasOne(pt => pt.Product)
                 .WithMany(p => p.ProductTags)
                 .HasForeignKey(pt => pt.ProductId);
            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ProductTags)
                .HasForeignKey(pt => pt.TagId);

            //Register all configurations(seeds, configurations)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            ////Example register single configuration
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.Entity<ProductFeature>().HasData(
                new ProductFeature() { Id = 1, Color = "Kırmızı", Height = 100, Width = 200, ProductId = 1 },
                new ProductFeature() { Id = 2, Color = "Sarı", Height = 100, Width = 300, ProductId = 2 },
                new ProductFeature() { Id = 3, Color = "Mavi", Height = 100, Width = 400, ProductId = 3 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
