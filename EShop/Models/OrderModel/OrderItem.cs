using EShop.Models.CouponModel;
using EShop.Models.Products;

namespace EShop.Models.OrderModel
{
    public class OrderItem
    {
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public int OrderId { get; set; }
        public int OptionId { get; set; }
        public virtual Order? Order { get; set; }
        
        public double? DiscountAmount { get; set; }
        public virtual Option? ProductOption { get; set; }
    }
}
