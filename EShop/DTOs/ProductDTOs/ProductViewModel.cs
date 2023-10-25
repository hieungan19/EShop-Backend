using EShop.DTOs.Image;
using EShop.DTOs.OptionDTOs;
using EShop.Models.CategoryModel;

namespace EShop.DTOs.ProductDTOs
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            this.OptionsIds = new List<int>();
            this.Images = new List<ImageViewModel>();
        }
        public double? MaxPrice { get; set; }
        public double? MinPrice { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<int> OptionsIds { get; set; }

        public List<OptionViewModel>? Options { get; set; }

        public List<ImageViewModel> Images { get; set; }
    }
}
