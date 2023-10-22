using EShop.DTOs.Account;
using EShop.Services.RegisterService;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    [Route("api/auth/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            this._registerService = registerService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel formData)
        {
            var result = await this._registerService.Register(formData, "User");

            if (result == "Success")
            {
                return Ok();
            }
            else if (result == "Existing email")
            {
                return StatusCode(422);
            }
            else
            {
                throw new Exception("Database Error");///
            }
        }
    }
}
