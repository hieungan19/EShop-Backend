using EShop.Models.Products;

namespace EShop.DTOs.OptionDTOs
{
    public class OptionViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool? IsSelected { get; set; }
        public Product? Product { get; set; }
    }
}
