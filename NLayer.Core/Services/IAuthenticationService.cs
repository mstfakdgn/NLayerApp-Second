using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IAuthenticationService : IService<User>
    {
        Task<CustomResponseDto<Token>> Login(string userName, string password);
        Task<CustomResponseDto<string>> Logout(string userName);
    }
}
