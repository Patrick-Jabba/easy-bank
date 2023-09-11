using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EasyBank.Api.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace EasyBank.Api.Domain.Services.Classes
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var keySecret = _configuration["KeySecret"];

            if(!string.IsNullOrEmpty(keySecret))
            {
                byte[] key = Encoding.UTF8.GetBytes(keySecret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new (ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                        new (ClaimTypes.Email, usuario.Email),
                    }),

                    Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration["HorasValidadeToken"])),
                    SigningCredentials = new(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                    ),
                };
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                
                return tokenHandler.WriteToken(token);
            }
            else 
            {
                return "Falha ao gerar token.";
            }

        }
    }
}