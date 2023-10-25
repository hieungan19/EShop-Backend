using EShop.Configuration;
using EShop.Configuration.Account;
using EShop.Configuration.Products;
using EShop.Models;
using EShop.Models.Account;
using EShop.Models.CartModel;
using EShop.Models.CategoryModel;
using EShop.Models.Products;
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
        public DbSet<Image> Images { get; set; }
        public DbSet<Cart> Carts { get; set; }
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
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
        }
    }

}
