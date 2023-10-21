using EShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EShop.Data
{
    public class EShopDBContext: DbContext
    {
        public DbSet<Test> Tests { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = LAPTOP-PP5O9HD5; Database = Test; Trusted_Connection = True; Integrated Security=True; TrustServerCertificate = True; MultipleActiveResultSets = True");
            }
        }
    }



    


}
