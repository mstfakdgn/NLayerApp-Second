using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    public interface IAuthenticationRepository : IGenericRepository<User>
    {
        Task<Token> Login(string userName, string password);
        Task<string> Logout(string userName);
    }
}
