using EShop.DTOs.Account;
using EShop.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace EShop.Services.RoleService
{
    public interface IRoleService
    {
        Task<RoleViewModel> GetRoleById(int roleId);
        string GetUserRole(int userId);
        Task<List<ApiRole>> GetAllRoles();
        Task<List<RoleViewModel>> GetListOfRoles();
        Task<ApiRole> GetRoleByName(string roleName);
        Task<ApiRole> GetRoleByUserId(int userId);
        Task<List<IdentityUserRole<int>>> GetUserRoles(int userId);
        bool IsUserInRole(int userId, int roleId);
        void UpdateUsersInRole(RoleViewModel formData);
    }
}
