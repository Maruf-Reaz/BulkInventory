using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using InquiadTradingApp.Models.Common.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using InquiadTradingApp.Models;

namespace InquiadTradingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
           
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = "a682b56a-6135-4111-a0k0-bdec547e3waz", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "da9a3b0e-8b6f-8529-71d0-4fd255e23f15", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has All Permissions" },
                new ApplicationRole { Id = "d925b59b-7456-1442-d0n0-bdec765e3awv", Name = "DataEntryOperator", NormalizedName = "DATAENTRYOPERAROR", ConcurrencyStamp = "ea9a3b0f-9b5f-7153-81e0-4fd799e23f17", CreatedOn = Convert.ToDateTime("2020-01-01"), Description = "Has Minimum Permissions" }
              
             );
            modelBuilder.Entity<UserType>().HasData(
              new UserType { Id = 1, Name = "Admin" },
              new UserType { Id = 2, Name = "DataEntryOperator" }
           );
               modelBuilder.Entity<WareHouse>().HasData(
              new WareHouse { Id = 1, Name = "In Ship", Type =0},
              new WareHouse { Id = 2, Name = "In Port",Type = 0 }
           );




            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser() { Id = "8ab6ee61-f36c-41b1-ae27-dbb23cbfb507", UserName = "Admin", NormalizedUserName = "ADMIN", Email = "test@mail.com", NormalizedEmail = "TEST@MAIL.COM", PasswordHash = "AQAAAAEAACcQAAAAEOJVsHUa611Khzkcg/zXgZ8EeegKhZAyW2eVPMzWJiToPuR45aBwuID99TNJ91JPxg==", SecurityStamp = "5TDMS5CNA2GYJK2URB4GDOCQI2NI7EHJ", ConcurrencyStamp = "26d21881-0a3a-44ab-aa4d-10664ace1e2d", UserTypeId=1 , Status=1}

               
                
             );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { UserId = "8ab6ee61-f36c-41b1-ae27-dbb23cbfb507", RoleId = "a682b56a-6135-4111-a0k0-bdec547e3waz" }
             );
            
        }

        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<WareHouse> WareHouses { get; set; }
        public DbSet<ProductCatagory> ProductCatagories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<PackSize> PackSizes { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<WareHouseTransfer> WareHouseTransfers { get; set; }
        public DbSet<PurchaseReturn> PurchaseReturns { get; set; }
        public DbSet<SaleReturn> SaleReturns { get; set; }
        public DbSet<LocalSale> LocalSales { get; set; }
        public DbSet<DamageProduct> DamageProducts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<RentWareHouse> RentWareHouses { get; set; }
        public DbSet<RentWareHouseItem> RentWareHouseItems { get; set; }

    }
}
