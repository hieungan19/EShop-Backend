using EShop.DTOs.Account;

namespace EShop.Services.RegisterService
{
    public interface IRegisterService
    {
        Task<string> Register(RegisterViewModel formData, string role);

    }
}
