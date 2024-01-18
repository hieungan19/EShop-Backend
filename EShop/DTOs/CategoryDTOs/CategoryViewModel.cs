using EShop.Models.Products;
namespace EShop.DTOs.CategoryDTOs

{
    public class CategoryViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product>? Products { get; set; } 
    }
}
