using EShop.Models.OrderModel;
using EShop.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)

        {
            builder.HasKey(oi => new { oi.OrderId, oi.OptionId }); 
            builder
                .HasOne<Order>(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            builder
                .HasOne<Option>(oi => oi.ProductOption)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.OptionId);
        }
    }
}
