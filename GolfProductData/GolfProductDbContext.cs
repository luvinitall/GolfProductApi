using System;
using GolfProductModel;
using Microsoft.EntityFrameworkCore;

namespace GolfProductData
{
    public class GolfProductDbContext:DbContext
    {
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Category> Categories { get; set; }

        public GolfProductDbContext(DbContextOptions<GolfProductDbContext> options)
        {
            Database.Migrate();
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
