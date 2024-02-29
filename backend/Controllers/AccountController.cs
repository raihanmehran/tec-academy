using backend.DTOs;
using backend.Entities;
using backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace backend.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        public AccountController(UserManager<AppUser> userManager, ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
            _userManager = userManager;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);

            if (user == null) return Unauthorized(new ProblemDetails { Title = "Invalid Email" });

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result) return Unauthorized(new ProblemDetails { Title = "Invalid Password" });

            var userDto = new UserDto
            {
                UserName = user.UserName,
                Country = user.Country,
                DateOfBirth = user.DateOfBirth.ToString(),
                Token = await _tokenRepository.CreateToken(user),
            };

            Log.Information("Login info => {@userDto}", userDto);

            return userDto;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async ValueTask<ActionResult<UserDto>> RegisterUser(RegisterUserDto registerUserDto)
        {
            if (await UserExists(userName: registerUserDto.UserName)) return
                BadRequest(new ProblemDetails { Title = "Email is taken please enter another email" });

            var user = new AppUser
            {
                UserName = registerUserDto.UserName,
                Country = registerUserDto.Country,
                DateOfBirth = (DateOnly)registerUserDto.DateOfBirth,
            };

            var result = await _userManager.CreateAsync(user: user,
                password: registerUserDto.Password);

            if (!result.Succeeded) return
                BadRequest(new ProblemDetails { Title = result.Errors.ToString() });

            var roleResult = await _userManager.AddToRoleAsync(user: user, role: "Member");

            if (!roleResult.Succeeded) return
                BadRequest(new ProblemDetails { Title = result.Errors.ToString() });

            return new UserDto
            {
                UserName = user.UserName,
                Country = user.Country,
                DateOfBirth = user.DateOfBirth.ToString(),
                Token = await _tokenRepository.CreateToken(user),
            };
        }

        private async ValueTask<bool> UserExists(string userName)
        {
            return await _userManager.Users.AnyAsync(user =>
                user.UserName == userName);
        }
    }
}