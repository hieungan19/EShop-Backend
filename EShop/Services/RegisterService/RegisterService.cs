using EShop.Data;
using EShop.DTOs.Account;
using EShop.Models.Account;
using EShop.Services.RoleService;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EShop.Services.RegisterService
{
    public class RegisterService : IRegisterService
    {
       
        private readonly EShopDBContext _context;
        private IRoleService _roleService;

        public RegisterService(EShopDBContext context, IRoleService roleService)
        {
            this._context = context;
            this._roleService = roleService;
        }

        private async Task<object> CheckExistingUser(ApiUser user)
        {
           var response =  await this._context.Users
                .Select(u => new { u.Id, u.Email })
                .Where(u => u.Email == user.Email)
                .FirstOrDefaultAsync();
            return response;
        }
        
        private async Task<bool> AssignRoleToUser(ApiUser user, string role)
        {
            var selectedRole = await this._roleService.GetRoleByName(role);
            if (selectedRole == null) { throw new Exception("Not Existing Role"); }

            var roleIdentity = new IdentityUserRole<int>()
            {
                UserId = user.Id,
                RoleId = selectedRole.Id
            };

            _context.UserRoles.Add(roleIdentity);

            return await this._context.SaveChangesAsync() >= 1 ? true : false;
        }


        public async Task<string> Register(RegisterViewModel formData, string role)
        {
            var user = new ApiUser()
            {
                Email = formData.Email,
                PasswordHash = new PasswordHasher()
                .HashPassword(formData.Password),
                FullName = formData.FullName,
                
            };

            if (await this.CheckExistingUser(user) != null)
            {
                return "Existing email";
            }

            this._context.Users.Add(user);
            await this._context.SaveChangesAsync();

            if (await this.AssignRoleToUser(user, role))
            {
                return "Success";
            }

            return "Error";
        }
    }
}
