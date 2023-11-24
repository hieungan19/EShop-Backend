
using EShop.Models.CategoryModel;
using EShop.Models.CouponModel;
using EShop.Models.ReviewModel;

namespace EShop.Models.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public string Description { get; set; }
        public int QuantitySold { get; set; }
        public int CategoryId { get; set; }
        public int? CurrentCouponId { get; set; }
        public string? ImageUrl { get; set; }
        public virtual Coupon? CurrentCoupon { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
