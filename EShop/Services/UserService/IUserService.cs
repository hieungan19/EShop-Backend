using EShop.DTOs.Account;

namespace EShop.Services.UserService
{
    public interface IUserService
    {
        Task<UserViewModel> GetUserById(int id);
        Task<UserListViewModel> GetUsers(int roleId );

        Task<bool> Update(EditUserViewModel userViewModel);

    }
}
