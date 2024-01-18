using EShop.Models;
using EShop.Services;
using EShop.Services.TestService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService ;

        public TestController(ITestService testService)
        {
            this._testService = testService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Test>>> GetAllTests ()
        {
            
            var response = await  this._testService.GetAll();
            
            if (response == null) return NotFound();
            return Ok(response);
        }
    }
}
