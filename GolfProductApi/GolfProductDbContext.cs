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
                .HasIndex(c => c.Sku).IsUnique(true).HasName("uidx_Product_Sku");
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


            modelBuilder.Entity<Family>().HasData(familyEpicDriver, familyRogueDriver, familyEpicIron, familyRogueIron, familyIronNoProducts, familyPutter);

 
            var productRogueIron3 =
                new Product() {ProductId = 1, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 3 Iron", Sku = "Rogue3IronMR", Gender = Gender.Male, Hand = Hand.Right };
            var productRogueIron4 =
                new Product() {ProductId = 2, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 4 Iron", Sku = "Rogue4IronMR", Gender = Gender.Male, Hand = Hand.Right };
            var productRogueIron5 =
                new Product() {ProductId = 3, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 5 Iron", Sku = "Rogue5IronMR", Gender = Gender.Male, Hand = Hand.Right};
            var productRogueIron6 =
                new Product() { ProductId = 4, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 6 Iron", Sku = "Rogue6IronMR", Gender = Gender.Male, Hand = Hand.Right};
            var productRogueIron7 =
                new Product() {ProductId = 5, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 7 Iron", Sku = "Rogue7IronMR", Gender = Gender.Male, Hand = Hand.Right };
            var productRogueIron8 =
                new Product() {ProductId = 6, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 8 Iron", Sku = "Rogue8IronMR", Gender = Gender.Male, Hand = Hand.Right};
            var productRogueIron9 =
                new Product() {ProductId = 7, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 9 Iron", Sku = "Rogue9IronMR", Gender = Gender.Male, Hand = Hand.Right};
            var productRogueIronSet1 =
                new Product() {ProductId = 8, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 3-P Iron", Sku = "Rogue3PIronMR", Gender = Gender.Male, Hand = Hand.Right };
            var productRogueIronSet2 =
                new Product() {ProductId = 9, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 5-P Iron", Sku = "Rogue5PIronMR", Gender = Gender.Male, Hand = Hand.Right };



            var productRogueIron5F =
                new Product() {ProductId = 10, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 5 Iron", Sku = "Rogue5IronWR", Gender = Gender.Female, Hand = Hand.Right };
            var productRogueIron6F =
                new Product() {ProductId = 11, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 6 Iron", Sku = "Rogue6IronWR", Gender = Gender.Female, Hand = Hand.Right };
            var productRogueIron7F =
                new Product() {ProductId = 12, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 7 Iron", Sku = "Rogue7IronWR", Gender = Gender.Female, Hand = Hand.Right };
            var productRogueIron8F =
                new Product() {ProductId = 13, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 8 Iron", Sku = "Rogue8IronWR", Gender = Gender.Female, Hand = Hand.Right };
            var productRogueIron9F =
                new Product() {ProductId = 14, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 9 Iron", Sku = "Rogue9IronWR", Gender = Gender.Female, Hand = Hand.Right };

            var productRogueIronSetF =
                new Product() {ProductId = 15, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 5-P Iron", Sku = "Rogue5PIronWR", Gender = Gender.Female, Hand = Hand.Right };


            var productRogueIron5LH =
                new Product() {ProductId = 16, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 5 Iron", Sku = "Rogue5IronLM", Gender = Gender.Male, Hand = Hand.Left};
            var productRogueIron6LH =
                new Product() {ProductId = 17, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 6 Iron", Sku = "Rogue6IronLM", Gender = Gender.Male, Hand = Hand.Left};
            var productRogueIron7LH =
                new Product() {ProductId = 18, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 7 Iron", Sku = "Rogue7IronLM", Gender = Gender.Male, Hand = Hand.Left};
            var productRogueIron8LH =
                new Product() {ProductId = 19, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 8 Iron", Sku = "Rogue8IronLM", Gender = Gender.Male, Hand = Hand.Left};
            var productRogueIron9LH =
                new Product() {ProductId = 20, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 9 Iron", Sku = "Rogue9IronLM", Gender = Gender.Male, Hand = Hand.Left};

            var productRogueIronSetLH =
                new Product() {ProductId = 21, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 5-P Iron", Sku = "Rogue5PIronLM", Gender = Gender.Male, Hand = Hand.Left};


            var productRogueIron5LHF =
                new Product() {ProductId = 22, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 5 Iron", Sku = "Rogue5IronLW", Gender = Gender.Female, Hand = Hand.Left };

            var productRogueIron7LHF =
                new Product() {ProductId = 23, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 7 Iron", Sku = "Rogue7IronLW", Gender = Gender.Female, Hand = Hand.Left};

            var productRogueIron9LHF =
                new Product() {ProductId = 24, FamilyId = familyRogueIron.FamilyId, Description = "Rogue 9 Iron", Sku = "Rogue9IronLW", Gender = Gender.Female, Hand = Hand.Left};


            modelBuilder.Entity<Product>().HasData(productRogueIron3,
                productRogueIron4,
                productRogueIron5,
                productRogueIron6,
                productRogueIron7,
                productRogueIron8,
                productRogueIron9,
                productRogueIronSet1,
                productRogueIronSet2,
                productRogueIron5F,
                productRogueIron6F,
                productRogueIron7F,
                productRogueIron8F,
                productRogueIron9F,
                productRogueIronSetF,
                productRogueIron5LH,
                productRogueIron6LH,
                productRogueIron7LH,
                productRogueIron8LH,
                productRogueIron9LH,
                productRogueIronSetLH,
                productRogueIron5LHF,
                productRogueIron7LHF,
                productRogueIron9LHF);

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
