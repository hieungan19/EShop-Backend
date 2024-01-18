namespace EShop.DTOs.ProductDTOs
{
    public class ProductListViewModel
    {
        public ProductListViewModel()
        {
            this.Products = new List<ProductViewModel>();
        }

        public List<ProductViewModel> Products { get; set; }
        public double TotalPages { get; set; }
    }

}
