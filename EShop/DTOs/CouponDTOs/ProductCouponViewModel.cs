namespace EShop.DTOs.CouponDTOs
{
    public class ProductCouponViewModel
    {
        public int ProductId { get; set; }
        public int CouponId { get; set; }   
        public string Name { get; set; }
        public double? DiscountPercent { get; set; }
        public double? DiscountAmount { get; set; }

    }
}
