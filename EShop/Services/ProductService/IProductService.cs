using EShop.DTOs;
using EShop.DTOs.ProductDTOs;
using EShop.Models.Products;

namespace EShop.Services.ProductService
{
    public interface IProductService
    {
        Product Create(ProductViewModel formData);
        Task<ProductViewModel> Update(int id, ProductViewModel formData);
        void Delete(int id);
        ProductViewModel GetProductById(int id);
        double GetTotalPages(int categoryId = 0, double priceFrom = 0, double priceTo = 0, int perPage = 0);
        IQueryable<Product> FilterQuery(IQueryable<Product> query, FilterViewModel filters);
        ProductListViewModel GetPaginatedProducts(FilterViewModel filters);

        

    }
}
