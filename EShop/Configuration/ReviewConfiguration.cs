using EShop.Models.Account;
using EShop.Models.Products;
using EShop.Models.ReviewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Configuration
{
    public class ReviewConfiguration: IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne<Product>(r => r.Product)
               .WithMany(p => p.Reviews)
               .HasForeignKey(r => r.ProductId);

            builder.HasOne<ApiUser>(r=>r.User).WithMany(u=>u.Reviews).HasForeignKey(r => r.UserId);
        }
    }
}
