using EShop.Models.OrderModel;

namespace EShop.Models.Products
{
    public class Option
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}
