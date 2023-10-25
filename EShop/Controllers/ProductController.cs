using EShop.DTOs;
using EShop.DTOs.ProductDTOs;
using EShop.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }
        [HttpGet]
        public ProductListViewModel GetPaginatedProducts([FromQuery] FilterViewModel filters)
        {
            return this._productService.GetPaginatedProducts(filters);
        }
        [HttpGet("{id}")]
        public ProductViewModel GetProductById(int id)
        {
            return _productService.GetProductById(id);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel formData)
        {
            var product = _productService.Create(formData);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ProductViewModel formData)
        {
            return Ok( await _productService.Update(formData));

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try
            {
                _productService.Delete(id);
                return Ok(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }

}
