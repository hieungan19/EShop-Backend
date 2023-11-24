using EShop.Data;
using EShop.DTOs.Account;
using EShop.Services.RoleService;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace EShop.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly EShopDBContext _context;
        private readonly IRoleService _roleService;

        public UserService(EShopDBContext context, IRoleService roleService)
        {
            this._context = context;
            this._roleService = roleService;
        }
        public async Task<UserViewModel> GetUserById(int id)
        {
            var user = await this._context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            var model = new UserViewModel();
            model.Id = user.Id;
            model.Email = user.Email;
            model.FullName = user.FullName;
            model.RoleName = this._roleService.GetUserRole(id);
            model.PhoneNumber = user.PhoneNumber;
            model.Address = user.Address;
            model.AvatarUrl = user.AvatarUrl; 
            return model;
        }

        public async Task<UserListViewModel> GetUsers(int roleId )
        {
            var users = await this._context.Users.ToListAsync();
            var model = new UserListViewModel();

            model.Users = users.Select(u => new UserViewModel
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName,
                RoleName = this._roleService.GetUserRole(u.Id),
                IsInRole = this._roleService.IsUserInRole(u.Id, roleId), 
                PhoneNumber = u.PhoneNumber,
                Address = u.Address,
                AvatarUrl = u.AvatarUrl,

            }).ToList();

            return model;
        }

        public async Task<bool> Update(EditUserViewModel formData)
        {
            var user = this._context.Users.Where(u => u.Id == formData.Id).FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            if (formData.Email != null)
            {
                user.Email = formData.Email;
            }
            if (formData.FullName != null)
            {
                user.FullName = formData.FullName;
            }

            if (formData.Address != null)
            {
                user.Address = formData.Address;
            }
            if (formData.AvatarUrl != null)
            {
                user.AvatarUrl = formData.AvatarUrl;
            }

            if (formData.OldPassword != "" && formData.NewPassword != "")
            {
                PasswordHasher hash = new PasswordHasher();
                var verifyOldPassword = hash.VerifyHashedPassword(user.PasswordHash, formData.OldPassword);

                if (verifyOldPassword == PasswordVerificationResult.Failed)
                {
                    return false;
                }

                var newPasswordHash = hash.HashPassword(formData.NewPassword);
                user.PasswordHash = newPasswordHash;
            }

            this._context.Entry(user).State = EntityState.Modified;
            this._context.SaveChanges();

            return true;
        }

    }
}
