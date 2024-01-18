using EShop.DTOs.Account;
using EShop.DTOs.OptionDTOs;
using EShop.Models.CouponModel;
using EShop.Models.OrderModel;

namespace EShop.DTOs.OrderDTOs
{
    public class OrderViewModel
    {
        public int? Id { get; set; }
        public string ReceiverName { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime? OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public int UserId { get; set; }
        public UserViewModel? UserInfo { get; set; }
        public string MobilePhone { get; set; }
        public OrderStatus Status { get; set; }
        public double DiscountAmount { get; set; }
        public int? CouponId { get; set; } 
        public bool IsPayed { get; set; }
        public OrderPaymentMethod PaymentMethod { get; set; }
        public Coupon? coupon { get; set; }
        public List<OptionViewModel> ItemsList { get; set; }
        public List<OrderItemViewModel>? OrderItems { get; set; } = new List<OrderItemViewModel>();
    }
}
