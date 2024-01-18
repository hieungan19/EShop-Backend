using EShop.Models.Account;
using EShop.Models.Products;

namespace EShop.Models.CartModel
{
    public class Cart
    {
        public int UserId { get; set; }
        public virtual ApiUser User { get; set; }

        public int OptionId { get; set; }
        public virtual Option Option { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
