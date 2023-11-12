using EShop.Data;
using EShop.DTOs.Account;
using EShop.JWT;
using EShop.Models.Account;
using EShop.Services.RoleService;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EShop.Services.LoginService
{
    public class LoginService : ILoginService
    {

        private readonly EShopDBContext _context;
        private readonly IRoleService _roleService;

        public LoginService(EShopDBContext context, IRoleService roleService)
        {
            this._context = context;
            this._roleService = roleService;
        }

        private async Task<List<Claim>> GetUserClaims(ICollection<IdentityUserRole<int>> roles)
        {
            List<ApiRole> roleNames = await this._roleService.GetAllRoles();
            var claims = new List<Claim>();

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,
                    roleNames.Find(r => r.Id == role.RoleId).Name));
            }

            claims.Add(new Claim("UserId", roles.First().UserId.ToString()));

            return claims;
        }

        private async Task<JwtSecurityToken> GenerateToken(ApiUser user)
        {
            var signingCredentials = new SigningCredentials(JWTConfig.SymmetricKey,
                SecurityAlgorithms.HmacSha256Signature);

            //Claims
            List<Claim> claims = await this.GetUserClaims(await this._roleService.GetUserRoles(user.Id));
            claims.Add(new Claim("sub", $"{user.Id}"));
            var token = new JwtSecurityToken(
                issuer: "EShop",
                audience: "EShop",
                expires: DateTime.Now.AddDays(7),
                signingCredentials: signingCredentials,
                claims: claims);

            return token;
        }

        public async Task<(string token, string message, UserViewModel? userView)> Attempt(string email, string password)
        {
            UserViewModel? userView = new UserViewModel();
            ApiUser user = await this._context.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return (null, "not-existing-user", userView);
            }

            var hash = new PasswordHasher();
            var result = hash.VerifyHashedPassword(user.PasswordHash, password);
            if (result == Microsoft.AspNet.Identity.PasswordVerificationResult.Failed)
            {
                return (null, "wrong-password", userView);
            }


            string token = new JwtSecurityTokenHandler().WriteToken(await this.GenerateToken(user));
            userView.Email = user.Email;
            userView.FullName = user.FullName;
            ApiRole role = await this._roleService.GetRoleByUserId(user.Id);

            userView.RoleName = role.Name;

            return (token, "sucess", userView);
        }

        public async Task<bool> SendPasswordResetToken(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            ApiUser? user = await _context.Users
               .Where(u => u.Email == email)
               .FirstOrDefaultAsync();

            return true;
        }

       
    }
}