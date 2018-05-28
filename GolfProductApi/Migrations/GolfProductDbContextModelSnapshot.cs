﻿// <auto-generated />
using GolfProductApi;
using GolfProductApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace GolfProductApi.Migrations
{
    [DbContext(typeof(GolfProductDbContext))]
    partial class GolfProductDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GolfProductApi.Entities.Catalog", b =>
                {
                    b.Property<short>("CatalogId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("CatalogId");

                    b.HasIndex("Description")
                        .IsUnique()
                        .HasName("uidx_Catalog_Description");

                    b.ToTable("Catalogs");
                });

            modelBuilder.Entity("GolfProductApi.Entities.CatalogCategory", b =>
                {
                    b.Property<short>("CatalogId");

                    b.Property<short>("CategoryId");

                    b.HasKey("CatalogId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("CatalogCategory");
                });

            modelBuilder.Entity("GolfProductApi.Entities.Category", b =>
                {
                    b.Property<short>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("CategoryId");

                    b.HasIndex("Description")
                        .IsUnique()
                        .HasName("uidx_Category_Description");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("GolfProductApi.Entities.Family", b =>
                {
                    b.Property<int>("FamilyId")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("CategoryId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("FamilyId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Description")
                        .IsUnique()
                        .HasName("uidx_Family_Description");

                    b.ToTable("Family");
                });

            modelBuilder.Entity("GolfProductApi.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<int>("FamilyId");

                    b.Property<byte>("Gender");

                    b.Property<byte>("Hand");

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasMaxLength(18);

                    b.HasKey("ProductId");

                    b.HasIndex("Description")
                        .IsUnique()
                        .HasName("uidx_Product_Sku");

                    b.HasIndex("FamilyId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("GolfProductApi.Entities.CatalogCategory", b =>
                {
                    b.HasOne("GolfProductApi.Entities.Catalog", "Catalog")
                        .WithMany("CatalogCategories")
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GolfProductApi.Entities.Category", "Category")
                        .WithMany("CatalogCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GolfProductApi.Entities.Family", b =>
                {
                    b.HasOne("GolfProductApi.Entities.Category", "Category")
                        .WithMany("Families")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GolfProductApi.Entities.Product", b =>
                {
                    b.HasOne("GolfProductApi.Entities.Family", "Family")
                        .WithMany("Products")
                        .HasForeignKey("FamilyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}