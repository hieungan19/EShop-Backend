using EShop.Data;
using EShop.DTOs.Account;
using EShop.Services.RoleService;
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
            return model;
        }

        public async Task<List<UserViewModel>> GetUsers(int roleId )
        {
            var users = await this._context.Users.ToListAsync();
            var model = new List<UserViewModel>();

            model = users.Select(u => new UserViewModel
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName,
                RoleName = this._roleService.GetUserRole(u.Id),
                IsInRole = this._roleService.IsUserInRole(u.Id, roleId)

            }).ToList();

            return model;
        }
    }
}
