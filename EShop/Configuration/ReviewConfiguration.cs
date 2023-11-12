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
            builder.HasKey(o => o.Id);

            builder.HasOne<Product>(o => o.Product)
               .WithMany(p => p.Reviews)
               .HasForeignKey(o => o.ProductId);


        }
    }
}
