using EShop.DTOs.Account;
using EShop.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    
    [Route("api/users")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<UserViewModel> GetUser(int id)
        {
            return await this._userService.GetUserById(id);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<UserListViewModel> GetUsers()
        {
            return  await this._userService.GetUsers(2);
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(EditUserViewModel formData)
        {
            //return updated user
            return Ok(await this._userService.Update(formData));
        }


    }
}
