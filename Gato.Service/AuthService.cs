using Gato.Core.Service_Contract;
using Gato.Repository.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Gato.Service
{
    public class AuthService : IAuthServices
    {
        private readonly IConfiguration _configuration;

        public AuthService( IConfiguration configuration )
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(User user, UserManager<User> userManager)
        {
            var Claims =new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName ,user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var UserRoles =await userManager.GetRolesAsync(user);

            foreach (var Role in UserRoles) 
                Claims.Add(new Claim(ClaimTypes.Role, Role));
            
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            var token = new JwtSecurityToken(
                audience: _configuration["JWT:ValidAudience"],
                issuer: _configuration["JWT:ValidIssuer"],   
                claims: Claims,
                expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
