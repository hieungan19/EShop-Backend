using Microsoft.AspNetCore.Identity;

namespace EShop.Models.Account 
{
    public class ApiUser: IdentityUser<int>
    {
        public string FullName { get; set; }
    }
}
