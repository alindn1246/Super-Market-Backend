﻿// <auto-generated />
using System;
using HussieniSuperMarket.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HussieniSuperMarket.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240427065716_FourthChange")]
    partial class FourthChange
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HussieniSuperMarket.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubCategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("HussieniSuperMarket.Models.MainCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MainCategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MainCategories");
                });

            modelBuilder.Entity("HussieniSuperMarket.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("HussieniSuperMarket.Models.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<byte[]>("ProductImages")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("HussieniSuperMarket.Models.SubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("MainCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("SubCategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MainCategoryId");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("HussieniSuperMarket.Models.Category", b =>
                {
                    b.HasOne("HussieniSuperMarket.Models.SubCategory", "SubCategories")
                        .WithMany("Categories")
                        .HasForeignKey("SubCategoryId");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("HussieniSuperMarket.Models.Product", b =>
                {
                    b.HasOne("HussieniSuperMarket.Models.Category", "Categories")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Categories");
                });

            modelBuilder.Entity("HussieniSuperMarket.Models.ProductImage", b =>
                {
                    b.HasOne("HussieniSuperMarket.Models.Product", "Products")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Products");
                });

            modelBuilder.Entity("HussieniSuperMarket.Models.SubCategory", b =>
                {
                    b.HasOne("HussieniSuperMarket.Models.MainCategory", "MainCategories")
                        .WithMany("SubCategories")
                        .HasForeignKey("MainCategoryId");

                    b.Navigation("MainCategories");
                });

            modelBuilder.Entity("HussieniSuperMarket.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("HussieniSuperMarket.Models.MainCategory", b =>
                {
                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("HussieniSuperMarket.Models.Product", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("HussieniSuperMarket.Models.SubCategory", b =>
                {
                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}
