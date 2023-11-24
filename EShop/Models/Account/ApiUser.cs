using EShop.Models.CartModel;
using EShop.Models.OrderModel;
using EShop.Models.ReviewModel;
using Microsoft.AspNetCore.Identity;

namespace EShop.Models.Account 
{
    public class ApiUser: IdentityUser<int>
    {
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? AvatarUrl { get; set; }
        public virtual ICollection<Cart> CartProductOptions { get; set; }
        public virtual ICollection<Order> Orders { get; set;  }
        public virtual ICollection<Review> Reviews { get; set; }

    }
}
