﻿using EShop.Configuration;
using EShop.Configuration.Account;
using EShop.Configuration.Products;
using EShop.Models;
using EShop.Models.Account;
using EShop.Models.CartModel;
using EShop.Models.CategoryModel;
using EShop.Models.CouponModel;
using EShop.Models.OrderModel;
using EShop.Models.Products;
using EShop.Models.ReviewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data
{
    public class EShopDBContext: IdentityDbContext<ApiUser, ApiRole, int>
    {
        public DbSet<Test> Tests { get; set; }
        public DbSet<ApiRole> Roles { get; set; }
        public DbSet<ApiUser> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }  
        public DbSet<Option> Options { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set;  }
        public DbSet<Coupon> Coupons { get; set; }
        
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = LAPTOP-PP5O9HD5; Database = EShop; Trusted_Connection = True; Integrated Security=True; TrustServerCertificate = True; MultipleActiveResultSets = True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ApiUserConfiguration());
            modelBuilder.ApplyConfiguration(new ApiRoleConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OptionConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());  
            modelBuilder.ApplyConfiguration(new CouponConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration()); 
           

        }
    }

}
