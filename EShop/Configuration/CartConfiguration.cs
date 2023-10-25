using EShop.Models.CartModel;
using EShop.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Configuration
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(c => new { c.UserId, c.OptionId });
            builder.HasOne<Option>(c => c.Option)
        .WithMany()
        .HasForeignKey(c => c.OptionId);
        }
    }
}
