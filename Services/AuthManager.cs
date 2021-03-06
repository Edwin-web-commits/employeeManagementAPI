
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
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

namespace EmployeeManagementAPI.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<User> _useManager;
        private readonly IConfiguration _configuration;
        private User _user;

        public AuthManager(UserManager<User> userManager, IConfiguration configuration)
        {
            _useManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var token = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        //generate token
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("lifetime").Value));

            var token = new JwtSecurityToken(
                 issuer: jwtSettings.GetSection("Issuer").Value,
                 claims: claims,
                 expires: expiration,
                 signingCredentials: signingCredentials
                );

            return token;
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
           {
               new Claim(ClaimTypes.Name, _user.FirstName )
           };

            var roles = await _useManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Environment.GetEnvironmentVariable("EMKEY");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        }

        public async Task<bool> ValidateUser(UserLoginDTO userDTO)
        {
            _user = await _useManager.FindByNameAsync(userDTO.Email);
            return (_user != null && await _useManager.CheckPasswordAsync(_user, userDTO.Password));
        }

        
    }
}
