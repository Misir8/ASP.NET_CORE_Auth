using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Contracts;
using WebApplication.DTOs;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController: ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            return await _accountService.Register(registerDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            return await _accountService.Login(loginDto);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public string Demo()
        {
            return "Secret Api";
        }
    }
}