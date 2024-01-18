using EShop.Models.Account;
using EShop.Models.CouponModel;
using EShop.Models.OrderModel;
using EShop.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Configuration
{
    public class OrderConfiguration: IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder
               .HasOne<ApiUser>(o => o.User)
               .WithMany(u => u.Orders)
               .HasForeignKey(o => o.UserId);

            builder
              .HasOne<Coupon>(o => o.Coupon)
              .WithMany(c => c.Orders)
              .HasForeignKey(o => o.CouponId);
        }
    }
}
