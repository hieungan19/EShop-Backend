using System.ComponentModel.DataAnnotations;

namespace EShop.Models.CouponModel
{
        public enum ApplyCouponType
    {

        [Display(Name = "Order")]
        Order,
        [Display(Name = "Option")] Option
        ,
        [Display(Name = "Product")]
        Product,
    }
    
}

