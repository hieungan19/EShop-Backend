using EShop.DTOs.CategoryDTOs;
using EShop.Models.CategoryModel;
using EShop.Models.Products;

namespace EShop.Services.CategoryService
{
    public interface ICategoryService
    {
        public CategoryListViewModel GetCategories();
        public CategoryViewModel GetCategoryById(int id);
        public Task<Category> Update(int id, CategoryViewModel formData);
        public int Create(CategoryViewModel formData);
        public void Delete(int id);
    }
}
