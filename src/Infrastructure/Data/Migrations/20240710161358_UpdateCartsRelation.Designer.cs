﻿// <auto-generated />
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240710161358_UpdateCartsRelation")]
    partial class UpdateCartsRelation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("CartProduct", b =>
                {
                    b.Property<int>("CartsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CartsId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("CartProducts", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("SellerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StockAvailable")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SellerId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Mates",
                            Name = "MATE STANLEY",
                            Price = 35000m,
                            SellerId = 3,
                            StockAvailable = 6
                        },
                        new
                        {
                            Id = 2,
                            Category = "Termos",
                            Name = "TERMO LUMILAGRO",
                            Price = 20000m,
                            SellerId = 3,
                            StockAvailable = 1
                        },
                        new
                        {
                            Id = 3,
                            Category = "Materas",
                            Name = "MOCHILA MATERA DE CUERO",
                            Price = 25000m,
                            SellerId = 3,
                            StockAvailable = 5
                        },
                        new
                        {
                            Id = 4,
                            Category = "Bombillas",
                            Name = "BOMBILLA",
                            Price = 6500m,
                            SellerId = 3,
                            StockAvailable = 3
                        });
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("UserType").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Entities.Client", b =>
                {
                    b.HasBaseType("Domain.Entities.User");

                    b.HasDiscriminator().HasValue("Client");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "cueton@example.com",
                            Password = "Cueton912",
                            UserName = "Cueton",
                            UserType = "Client"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Seller", b =>
                {
                    b.HasBaseType("Domain.Entities.User");

                    b.HasDiscriminator().HasValue("Seller");

                    b.HasData(
                        new
                        {
                            Id = 3,
                            Email = "miguel@example.com",
                            Password = "Miguelito3520",
                            UserName = "Miguel",
                            UserType = "Seller"
                        });
                });

            modelBuilder.Entity("Domain.Entities.SysAdmin", b =>
                {
                    b.HasBaseType("Domain.Entities.User");

                    b.HasDiscriminator().HasValue("Admin");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Email = "admin@example.com",
                            Password = "Admin123",
                            UserName = "admin",
                            UserType = "Admin"
                        });
                });

            modelBuilder.Entity("CartProduct", b =>
                {
                    b.HasOne("Domain.Entities.Cart", null)
                        .WithMany()
                        .HasForeignKey("CartsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Cart", b =>
                {
                    b.HasOne("Domain.Entities.Client", null)
                        .WithOne("Carts")
                        .HasForeignKey("Domain.Entities.Cart", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.HasOne("Domain.Entities.Seller", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("Domain.Entities.Client", b =>
                {
                    b.Navigation("Carts")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
