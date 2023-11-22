using EShop.Data;
using EShop.DTOs.CategoryDTOs;
using EShop.Models.CategoryModel;
using EShop.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace EShop.Services.CategoryService
{
    public class CategoryService:ICategoryService
    {
        private readonly EShopDBContext _context;
        public CategoryService(EShopDBContext context)
        {
            this._context = context;
        }
        public CategoryViewModel GetCategoryById(int id)
        {
            var category = _context.Categories.Include(c=>c.Products).FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                throw new Exception();
            }

            var model = new CategoryViewModel();

            model.Id = category.Id;
            model.Name = category.Name;
            model.Products = category.Products; 

            return model;
        }
        public CategoryListViewModel GetCategories()
        {
            var query = _context.Categories.AsQueryable();

            CategoryListViewModel model=new CategoryListViewModel();
            model.Categories = query.Select(c => new CategoryViewModel() { Id = c.Id, Name = c.Name }).ToList();
            return model; 

            //.Include(c => c.Products);
        }
        public int Create(CategoryViewModel formData)
        {
            var category = new Category()
            {
                Name = formData.Name,
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
            int id = category.Id;
            return id; 
        }
        public async Task<Category> Update(int id, CategoryViewModel formData)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                throw new Exception();
            }

            category.Name = formData.Name;
            Console.WriteLine(category.Name);

           await  _context.SaveChangesAsync();
            return category; 
           
            
        }
        public void Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                throw new Exception();
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

       
    }
}
