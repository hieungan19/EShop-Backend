using System.ComponentModel.DataAnnotations;

namespace EShop.Models.OrderModel
{
    public enum OrderStatus
    {

        [Display(Name = "Pending")]
        Pending,
        [Display(Name = "Shipped")]
        Shipped,
        [Display(Name = "Finished")]
        Finished,
        [Display(Name = "Cancelled")]
        Cancelled

    }
}
