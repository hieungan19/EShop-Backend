using EShop.Models.Account;
using EShop.Models.CouponModel;

namespace EShop.Models.OrderModel
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; } 
        public OrderPaymentMethod PaymentMethod { get; set; }
        public bool IsPayed { get; set; } = false;
        public double DiscountAmount { get; set; }  
        public string ShippingAddress { get; set; }
        public string MobilePhone { get; set; }
        public OrderStatus Status { get; set; }
        public int UserId { get; set; }
        public virtual ApiUser User { get; set; }
        public int? CouponId { get; set; }
        public virtual Coupon? Coupon { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
