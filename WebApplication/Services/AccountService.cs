using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.DTOs;
using WebApplication.Exceptions;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class AccountService: IAccountService
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) throw new HttpException(HttpStatusCode.BadRequest,"Username is taken");

            var user = _mapper.Map<AppUser>(registerDto);

            user.UserName = registerDto.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) throw new HttpException(HttpStatusCode.BadRequest, result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");

            if (!roleResult.Succeeded) throw new HttpException(HttpStatusCode.BadRequest, result.Errors);;

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Displayname = user.Firstname + " " + user.Lastname
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) throw new HttpException(HttpStatusCode.Unauthorized, "Invalid username");

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) throw new HttpException(HttpStatusCode.Unauthorized);

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user),
                Displayname = user.Firstname + " " + user.Lastname
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}