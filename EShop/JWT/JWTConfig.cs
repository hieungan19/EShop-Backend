using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EShop.JWT
{
    public class JWTConfig
    {
        private static string secureKey = "securityKey-hashed-0109@2003**##-hieu-ngan-ne";// set in local variable
        public static SymmetricSecurityKey SymmetricKey { get; } = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
    }
}
