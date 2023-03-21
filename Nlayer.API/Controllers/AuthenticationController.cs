using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Helpers;

namespace Nlayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authentication_service;

        public AuthenticationController(IMapper mapper, IAuthenticationService authentication_service)
        {
            _mapper = mapper;
            _authentication_service = authentication_service;
        }

        [AllowAnonymous]
        [HttpGet("[action]/{userName}/{password}")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            return CreateActionResult(await _authentication_service.Login(userName, password));
        }

        [HttpGet("[action]/{userName}")]
        public async Task<IActionResult> Logout(string userName)
        {
            return CreateActionResult(await _authentication_service.Logout(userName));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Save(CreateUserDto request)
        {
            var user = new User { 
                UserName = request.UserName,
                Password = Helper.HashPassword(request.Password)
            };

            return CreateActionResult(CustomResponseDto<User>.Success(201, await _authentication_service.AddAsync(user)));
        }
    }
}
