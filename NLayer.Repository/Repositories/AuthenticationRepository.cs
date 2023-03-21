using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class AuthenticationRepository : GenericRepository<User>, IAuthenticationRepository
    {
        private readonly IConfiguration _iconfiguration;
        public AuthenticationRepository(AppDbContext context, IConfiguration iconfiguration) : base(context)
        {
            _iconfiguration = iconfiguration;
        }

        public async Task<Token> Login(string userName, string password)
        {
            var user = await _context.Users.Where(x => x.UserName == userName && x.Password == password).FirstOrDefaultAsync();

            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                      {
                     new Claim(ClaimTypes.Name, user.UserName)
                    }),
                    Expires = DateTime.UtcNow.AddDays(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new Token { TokenBody = tokenHandler.WriteToken(token) };
            }
            else
            {
                return null;
            }
        }

        public Task<string> Logout(string userName)
        {
            throw new NotImplementedException();
        }


    }
}
