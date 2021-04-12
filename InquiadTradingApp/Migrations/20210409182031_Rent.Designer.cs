﻿// <auto-generated />
using System;
using InquiadTradingApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InquiadTradingApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210409182031_Rent")]
    partial class Rent
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InquiadTradingApp.Models.Broker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNo");

                    b.HasKey("Id");

                    b.ToTable("Brokers");
                });

            modelBuilder.Entity("InquiadTradingApp.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNo");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("InquiadTradingApp.Models.Common.Authentication.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "a682b56a-6135-4111-a0k0-bdec547e3waz",
                            ConcurrencyStamp = "da9a3b0e-8b6f-8529-71d0-4fd255e23f15",
                            CreatedOn = new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Has All Permissions",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "d925b59b-7456-1442-d0n0-bdec765e3awv",
                            ConcurrencyStamp = "ea9a3b0f-9b5f-7153-81e0-4fd799e23f17",
                            CreatedOn = new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Has Minimum Permissions",
                            Name = "DataEntryOperator",
                            NormalizedName = "DATAENTRYOPERAROR"
                        });
                });

            modelBuilder.Entity("InquiadTradingApp.Models.Common.Authentication.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<byte>("Status");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<int>("UserTypeId");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UserTypeId");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "8ab6ee61-f36c-41b1-ae27-dbb23cbfb507",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "26d21881-0a3a-44ab-aa4d-10664ace1e2d",
                            Email = "test@mail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "TEST@MAIL.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEOJVsHUa611Khzkcg/zXgZ8EeegKhZAyW2eVPMzWJiToPuR45aBwuID99TNJ91JPxg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "5TDMS5CNA2GYJK2URB4GDOCQI2NI7EHJ",
                            Status = (byte)1,
                            TwoFactorEnabled = false,
                            UserName = "Admin",
                            UserTypeId = 1
                        });
                });

            modelBuilder.Entity("InquiadTradingApp.Models.Common.Authentication.UserType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("UserTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "DataEntryOperator"
                        });
                });

            modelBuilder.Entity("InquiadTradingApp.Models.DamageProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DamageDate");

                    b.Property<double>("KGAmount");

                    b.Property<double>("PackAmount");

                    b.Property<int>("PurchaseId");

                    b.Property<string>("Remarks");

                    b.Property<int>("Status");

                    b.Property<double>("TonAMount");

                    b.Property<string>("VoucharNo");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseId");

                    b.ToTable("DamageProducts");
                });

            modelBuilder.Entity("InquiadTradingApp.Models.LocalSale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<string>("ClientName");

                    b.Property<double>("PackAmount");

                    b.Property<int>("PurchaseId");

                    b.Property<string>("Remarks");

                    b.Property<DateTime>("SaleDate");

                    b.Property<int>("Status");

                    b.Property<double>("TonAMount");

                    b.Property<string>("VoucharNo");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseId");

                    b.ToTable("LocalSales");
                });

            modelBuilder.Entity("InquiadTradingApp.Models.PackSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<double>("TotalKilo");

                    b.HasKey("Id");

                    b.ToTable("PackSizes");
                });

            modelBuilder.Entity("InquiadTradingApp.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CheckNo");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Details");

                    b.Property<double>("PaidAmount");

                    b.Property<int?>("PurchaseId");

                    b.Property<int?>("SaleId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseId");

                    b.HasIndex("SaleId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("InquiadTradingApp.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("ProductCatagoryId");

                    b.HasKey("Id");

                    b.HasIndex("ProductCatagoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("InquiadTradingApp.Models.ProductCatagory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ProductCatagories");
                });

            modelBuilder.Entity("InquiadTradingApp.Models.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("PackAmount");

                    b.Property<int>("PackSizeId");

                    b.Property<int>("ProductId");

                    b.Property<DateTime>("PurchaseDate");

                    b.Property<string>("Remarks");

                    b.Property<int>("Status");

                    b.Property<double>("TonAmount");

                    b.Property<double>("TotalAmount");

                    b.Property<int?>("VendorId");

                    b.Property<string>("VendorWareWhouse");

                    b.Property<string>("VoucharNo");

                    b.Property<int>("WareHouseId");

                    b.HasKey("Id");

                    b.HasIndex("PackSizeId");

                    b.HasIndex("ProductId");

                    b.HasIndex("VendorId");

                    b.HasIndex("WareHouseId");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("InquiadTradingApp.Models.PurchaseReturn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<double>("PackAmount");

                    b.Property<int>("PurchaseId");

                    b.Property<string>("Remarks");

                    b.Property<DateTime>("ReturnDate");

                    b.Property<int>("Status");

                    b.Property<double>("TonAmount");

                    b.Property<string>("VoucharNo");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseId");

                    b.ToTable("PurchaseReturns");
                });

            modelBuilder.Entity("InquiadTradingApp.Models.RentWareHouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Owner");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("RentWareHouses");
                });

            modelBuilder.Entity("InquiadTradingApp.Models.RentWareHouseItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<string>("Details");

                    b.Property<DateTime>("FromDate");

                    b.Property<int>("PurchaseId");

                    b.Property<int>("RentWareHouseId");

                    b.Property<DateTime>("ToDate");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseId");

                    b.HasIndex("RentWareHouseId");

                    b.ToTable("RentWareHouseItems");
                });

            modelBuilder.Entity("InquiadTradingApp.Models.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("BrokerAMount");

                    b.Property<int>("BrokerId");

                    b.Property<int>("ClientId");

                    b.Property<string>("ClientWareHouse");

                    b.Property<double>("PackAmount");

                    b.Property<int>("PurchaseId");

                    b.Property<string>("Remarks");

                    b.Property<DateTime>("SaleDate");

                    b.Property<int>("Status");

                    b.Property<double>("TonAmount");

                    b.Property<double>("TotalAmount");

                    b.Property<string>("VoucharNo");

                    b.HasKey("Id");

                    b.HasIndex("BrokerId");

                    b.HasIndex("ClientId");

                    b.HasIndex("PurchaseId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("InquiadTradingApp.Models.SaleReturn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<double>("PackAmount");

                    b.Property<string>("Remarks");

                    b.Property<DateTime>("ReturnDate");

                    b.Property<int>("SaleId");

                    b.Property<int>("Status");

                    b.Property<double>("TonAmount");

                    b.Property<string>("VoucharNo");

                    b.HasKey("Id");

                    b.HasIndex("SaleId");

                    b.ToTable("SaleReturns");
                });

            modelBuilder.Entity("InquiadTradingApp.Models.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNo");

                    b.HasKey("Id");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("InquiadTradingApp.Models.WareHouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Name");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("WareHouses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "In Ship",
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            Name = "In Port",
                            Type = 0
                        });
                });

            modelBuilder.Entity("InquiadTradingApp.Models.WareHouseTransfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PurchaseId");

                    b.Property<string>("Remarks");

                    b.Property<int>("Status");

                    b.Property<int>("ToWareHouseId");

                    b.Property<string>("TransactionNo");

                    b.Property<double>("TransferAmountInTons");

                    b.Property<double>("TransferAmountPackSize");

                    b.Property<DateTime>("TransferDate");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseId");

                    b.HasIndex("ToWareHouseId");

                    b.ToTable("WareHouseTransfers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "8ab6ee61-f36c-41b1-ae27-dbb23cbfb507",
                            RoleId = "a682b56a-6135-4111-a0k0-bdec547e3waz"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("InquiadTradingApp.Models.Common.Authentication.ApplicationUser", b =>
                {
                    b.HasOne("InquiadTradingApp.Models.Common.Authentication.UserType", "UserType")
                        .WithMany("ApplicationUsers")
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InquiadTradingApp.Models.DamageProduct", b =>
                {
                    b.HasOne("InquiadTradingApp.Models.Purchase", "Purchase")
                        .WithMany()
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InquiadTradingApp.Models.LocalSale", b =>
                {
                    b.HasOne("InquiadTradingApp.Models.Purchase", "Purchase")
                        .WithMany()
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InquiadTradingApp.Models.Payment", b =>
                {
                    b.HasOne("InquiadTradingApp.Models.Purchase", "Purchase")
                        .WithMany("Payments")
                        .HasForeignKey("PurchaseId");

                    b.HasOne("InquiadTradingApp.Models.Sale", "Sale")
                        .WithMany("Payments")
                        .HasForeignKey("SaleId");
                });

            modelBuilder.Entity("InquiadTradingApp.Models.Product", b =>
                {
                    b.HasOne("InquiadTradingApp.Models.ProductCatagory", "ProductCatagory")
                        .WithMany("Products")
                        .HasForeignKey("ProductCatagoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InquiadTradingApp.Models.Purchase", b =>
                {
                    b.HasOne("InquiadTradingApp.Models.PackSize", "PackSize")
                        .WithMany()
                        .HasForeignKey("PackSizeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InquiadTradingApp.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InquiadTradingApp.Models.Vendor", "Vendor")
                        .WithMany()
                        .HasForeignKey("VendorId");

                    b.HasOne("InquiadTradingApp.Models.WareHouse", "WareHouse")
                        .WithMany()
                        .HasForeignKey("WareHouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InquiadTradingApp.Models.PurchaseReturn", b =>
                {
                    b.HasOne("InquiadTradingApp.Models.Purchase", "Purchase")
                        .WithMany()
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InquiadTradingApp.Models.RentWareHouseItem", b =>
                {
                    b.HasOne("InquiadTradingApp.Models.Purchase", "Purchase")
                        .WithMany()
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InquiadTradingApp.Models.RentWareHouse", "RentWareHouse")
                        .WithMany("RentWareHouseItems")
                        .HasForeignKey("RentWareHouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InquiadTradingApp.Models.Sale", b =>
                {
                    b.HasOne("InquiadTradingApp.Models.Broker", "Broker")
                        .WithMany()
                        .HasForeignKey("BrokerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InquiadTradingApp.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InquiadTradingApp.Models.Purchase", "Purchase")
                        .WithMany()
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InquiadTradingApp.Models.SaleReturn", b =>
                {
                    b.HasOne("InquiadTradingApp.Models.Sale", "Sale")
                        .WithMany()
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InquiadTradingApp.Models.WareHouseTransfer", b =>
                {
                    b.HasOne("InquiadTradingApp.Models.Purchase", "Purchase")
                        .WithMany()
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InquiadTradingApp.Models.WareHouse", "ToWareHouse")
                        .WithMany()
                        .HasForeignKey("ToWareHouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("InquiadTradingApp.Models.Common.Authentication.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("InquiadTradingApp.Models.Common.Authentication.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("InquiadTradingApp.Models.Common.Authentication.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("InquiadTradingApp.Models.Common.Authentication.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InquiadTradingApp.Models.Common.Authentication.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("InquiadTradingApp.Models.Common.Authentication.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
