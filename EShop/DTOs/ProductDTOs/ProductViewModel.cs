using EShop.DTOs.Image;
using EShop.DTOs.OptionDTOs;
using EShop.DTOs.ReviewDTOs;
using EShop.Models.CategoryModel;
using EShop.Models.CouponModel;

namespace EShop.DTOs.ProductDTOs
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            this.OptionsIds = new List<int>();

        }
        public double? MaxPrice { get; set; }
        public double? MinPrice { get; set; }

        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public int? CurrentCouponId { get; set; }
        public double? CurrentMaxPrice { get; set; }  
        public double? CurrentMinPrice { get; set; }
        public int? QuantitySold { get; set; }
        public Coupon? CurrentCoupon { get; set; }
        public Category? Category { get; set; }
        public List<int>? OptionsIds { get; set; }
        public string? ImageUrl { get; set; }
        public double AverageStar { get; set; } = 0;
        public List<ReviewViewModel>? Reviews { get; set; }
        public List<OptionViewModel>? Options { get; set; }

    }
}
