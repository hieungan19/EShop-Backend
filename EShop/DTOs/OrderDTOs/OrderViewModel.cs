using EShop.DTOs.OptionDTOs;
using EShop.Models.CouponModel;
using EShop.Models.OrderModel;

namespace EShop.DTOs.OrderDTOs
{
    public class OrderViewModel
    {
        public int? Id { get; set; }
        public double? TotalPrice { get; set; }
        public string OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public int UserId { get; set; }
        public string MobilePhone { get; set; }
        public string? Status { get; set; }
        public double DiscountAmount { get; set; }
        public int? CouponId { get; set; }  
        public Coupon? coupon { get; set; }
        public List<OptionViewModel> ItemsList { get; set; }
        public List<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
    }
}
