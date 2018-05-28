using System;
using GolfProductApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace GolfProductApi
{
    public class GolfProductDbContext:DbContext
    {
        public virtual DbSet<Catalog> Catalogs { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Category> Families { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public GolfProductDbContext(DbContextOptions<GolfProductDbContext> options)
        {
            //This will execute the database migrations
            //including creating or updating the database according to the model
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalog>()
                .HasIndex(c => c.Description).IsUnique(true).HasName("uidx_Catalog_Description");
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Description).IsUnique(true).HasName("uidx_Category_Description");
            modelBuilder.Entity<Family>()
                .HasIndex(c => c.Description).IsUnique(true).HasName("uidx_Family_Description");
            modelBuilder.Entity<Product>()
                .HasIndex(c => c.Description).IsUnique(true).HasName("uidx_Product_Sku");
            modelBuilder.Entity<CatalogCategory>()
                .HasKey(t => new { t.CatalogId, t.CategoryId });


    }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=GolfProductDb;Trusted_Connection=True");
            }
        }
    }
}
