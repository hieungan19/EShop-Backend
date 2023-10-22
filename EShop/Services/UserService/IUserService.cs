using EShop.DTOs.Account;

namespace EShop.Services.UserService
{
    public interface IUserService
    {
        Task<UserViewModel> GetUserById(int id);
        Task<List<UserViewModel>> GetUsers(int roleId );

    }
}
