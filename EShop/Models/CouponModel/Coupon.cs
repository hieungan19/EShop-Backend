namespace EShop.Models.CouponModel
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Desciption { get; set; }
        public ApplyCouponType ApplyCouponType { get; set; }
        public double DiscountPercent { get; set; }
        public double DiscountAmount { get; set; }  
        public double MaxDiscountAmount { get; set; }   
        public double MinBillAmount { get; set; }   
      
    }
}
