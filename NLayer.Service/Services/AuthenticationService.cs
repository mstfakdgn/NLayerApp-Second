using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using NLayer.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class AuthenticationService : Service<User>, IAuthenticationService
    {
        protected readonly IAuthenticationRepository _authentication_repository;

        public AuthenticationService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IAuthenticationRepository authentication_repository) : base(repository, unitOfWork)
        {
            _authentication_repository = authentication_repository;
        }

        public async Task<CustomResponseDto<Token>> Login(string userName, string password)
        {
            string hashedPassword = Helper.HashPassword(password);

            var token = await _authentication_repository.Login(userName, hashedPassword);

            if (token == null)
            {
                throw new NotFoundException($"({userName}) username or password is wrong");
            }

            return CustomResponseDto<Token>.Success(201, token);
        }

        public Task<CustomResponseDto<string>> Logout(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
