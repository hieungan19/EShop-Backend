using EShop.DTOs.Account;

namespace EShop.Services.LoginService
{
    public interface ILoginService
    {
        Task<(string token,  string message, UserViewModel userView)> Attempt(string email, string password);
        Task<bool> SendPasswordResetToken(string email); 
        
    }
}
