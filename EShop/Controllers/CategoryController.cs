using EShop.DTOs.CategoryDTOs;
using EShop.Services.CategoryService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    [Route("api/categories")]
    [ApiController]


    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            return _categoryService.GetCategories();
        }

        [HttpGet("{id}")]
        public async Task<CategoryViewModel> GetCategoryById(int id)
        {
            return _categoryService.GetCategoryById(id);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel formData)
        {
           
            var newId =  _categoryService.Create(formData);
            formData.Id = newId;
            return CreatedAtAction(nameof(GetCategoryById), new { id = newId }, formData);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryViewModel formData)
        {
            _categoryService.Update(id, formData);
            return Ok(_categoryService.GetCategoryById(id)); 
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _categoryService.Delete(id);
            return NoContent();
        }
    }
}
