using EShop.Models.Account;
using EShop.Models.Products;

namespace EShop.Models.ReviewModel
{
    public class Review
    {
        public int Id { get; set; }
        public string? Detail { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public double Star { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }    
        public virtual ApiUser? User { get; set; }

    }
}
