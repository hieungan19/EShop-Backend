using EShop.Models.CouponModel;

namespace EShop.DTOs.CouponDTOs
{
    public class CouponViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Desciption { get; set; }
        public string ApplyCouponType { get; set; }
        public double? DiscountPercent { get; set; }
        public double? DiscountAmount { get; set; }
        public double? MaxDiscountAmount { get; set; }
        public double? MinBillAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
