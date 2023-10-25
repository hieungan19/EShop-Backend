using EShop.DTOs.OptionDTOs;
using EShop.Services.OptionServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    [Route("api/options")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly IOptionService _optionService;
        public OptionController(IOptionService propertyService)
        {
            this._optionService = propertyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            return Ok(_optionService.GetOptionById(id));
        }


        [HttpPost]
        public async Task<IActionResult> Create(OptionViewModel formData)
        {
            
            return Ok(_optionService.Create(formData));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(OptionViewModel formData)
        {
            return Ok(_optionService.Update(formData));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            this._optionService.Delete(id);
            return NoContent();
        }
    }
}
