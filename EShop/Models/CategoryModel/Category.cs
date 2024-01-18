using EShop.Models.Products;

namespace EShop.Models.CategoryModel
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
