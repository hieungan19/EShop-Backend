
using EShop.Models.CategoryModel;

namespace EShop.Models.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
