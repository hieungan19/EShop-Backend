using EShop.DTOs.CategoryDTOs;
using EShop.Models.CategoryModel;
using EShop.Models.Products;

namespace EShop.Services.CategoryService
{
    public interface ICategoryService
    {
        public List<CategoryViewModel> GetCategories();
        public CategoryViewModel GetCategoryById(int id);
        public void Update(int id, CategoryViewModel formData);
        public int Create(CategoryViewModel formData);
        public void Delete(int id);
    }
}
