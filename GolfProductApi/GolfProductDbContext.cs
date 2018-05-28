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

            InitializeSeedData(modelBuilder);


        }

        private void InitializeSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalog>().HasData(new Catalog() { CatalogId = 1, Description = "US Catalog"});
            modelBuilder.Entity<Catalog>().HasData(new Catalog() { CatalogId = 2, Description = "Japan Catalog" });

            Category categoryWoods = new Category() {CategoryId = 1, Description = "Woods"};
            Category categoryIrons = new Category() { CategoryId = 2, Description = "Irons" };
            Category categoryPutters= new Category() { CategoryId = 3, Description = "Putters" };


            modelBuilder.Entity<Category>().HasData(categoryWoods,categoryIrons, categoryPutters);

            //add all data for US Catalog
            modelBuilder.Entity<CatalogCategory>()
                .HasData(new CatalogCategory() {CatalogId = 1, CategoryId = categoryWoods.CategoryId});
            modelBuilder.Entity<CatalogCategory>()
                .HasData(new CatalogCategory() {CatalogId = 1, CategoryId = categoryIrons.CategoryId});
            modelBuilder.Entity<CatalogCategory>()
                .HasData(new CatalogCategory() {CatalogId = 1, CategoryId = categoryPutters.CategoryId});


            //just add woods and irons for Japan (no putters)
            modelBuilder.Entity<CatalogCategory>()
                .HasData(new CatalogCategory() { CatalogId = 2, CategoryId = categoryWoods.CategoryId });
            modelBuilder.Entity<CatalogCategory>()
                .HasData(new CatalogCategory() { CatalogId = 2, CategoryId = categoryIrons.CategoryId });

            var familyEpicDriver = new Family()
            {
                FamilyId = 1,
                Description = "Epic Drivers",
                CategoryId = categoryWoods.CategoryId
            };

            var familyRogueDriver = new Family()
            {
                FamilyId = 2,
                Description = "Rogue Drivers",
                CategoryId = categoryWoods.CategoryId
            };

            var familyEpicIron = new Family()
            {
                FamilyId = 3,
                Description = "Epic Irons",
                CategoryId = categoryIrons.CategoryId
            };

            var familyRogueIron = new Family()
            {
                FamilyId = 4,
                Description = "Rogue Irons",
                CategoryId = categoryIrons.CategoryId
            };

            

            var familyIronNoProducts = new Family()
            {
                FamilyId = 5,
                Description = "Empty Family",
                CategoryId = categoryIrons.CategoryId
            };

            var familyPutter = new Family()
            {
                FamilyId = 6,
                Description = "Toulon Putter",
                CategoryId = categoryPutters.CategoryId
            };


            //modelBuilder.Entity<Catalog>().HasData(new Catalog() { CatalogId = 2, Description = "Japan Catalog" });


            //var productRogueIron3 =
            //    new Model.Product() { Description = "Rogue 3 Iron", Family = familyRogueIron, Sku = "Rogue3IronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };
            //var productRogueIron4 =
            //    new Model.Product() { Description = "Rogue 4 Iron", Family = familyRogueIron, Sku = "Rogue4IronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };
            //var productRogueIron5 =
            //    new Model.Product() { Description = "Rogue 5 Iron", Family = familyRogueIron, Sku = "Rogue5IronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };
            //var productRogueIron6 =
            //    new Model.Product() { Description = "Rogue 6 Iron", Family = familyRogueIron, Sku = "Rogue6IronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };
            //var productRogueIron7 =
            //    new Model.Product() { Description = "Rogue 7 Iron", Family = familyRogueIron, Sku = "Rogue7IronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };
            //var productRogueIron8 =
            //    new Model.Product() { Description = "Rogue 8 Iron", Family = familyRogueIron, Sku = "Rogue8IronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };
            //var productRogueIron9 =
            //    new Model.Product() { Description = "Rogue 9 Iron", Family = familyRogueIron, Sku = "Rogue9IronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };
            //var productRogueIronSet1 =
            //    new Model.Product() { Description = "Rogue 3-P Iron", Family = familyRogueIron, Sku = "Rogue3PIronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };
            //var productRogueIronSet2 =
            //    new Model.Product() { Description = "Rogue 5-P Iron", Family = familyRogueIron, Sku = "Rogue5PIronMR", Gender = Gender.Male, Hand = Hand.Right, ProductGroup = rogueIronRHMensProductGroup };
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
