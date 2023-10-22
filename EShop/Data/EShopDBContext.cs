using EShop.Configuration.Account;
using EShop.Models;
using EShop.Models.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data
{
    public class EShopDBContext: IdentityDbContext<ApiUser, ApiRole, int>
    {
        public DbSet<Test> Tests { get; set; }
        public DbSet<ApiRole> Roles { get; set; }
        public DbSet<ApiUser> Users { get; set; }
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
        }
    }



    


}
