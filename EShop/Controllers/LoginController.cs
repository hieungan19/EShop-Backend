using EShop.DTOs.Account;
using EShop.Services.LoginService;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    [Route("api/auth/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            this._loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel formData)
        {
            var (token, message, userView) = await this._loginService.Attempt(formData.Email, formData.Password);
            if (token == null)
            {
                return NotFound(message);
            }

            return Ok(new LoginResponseViewModel(token, message, userView));
        }
    }
}
