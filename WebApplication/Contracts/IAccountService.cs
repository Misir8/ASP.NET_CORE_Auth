using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DTOs;

namespace WebApplication.Contracts
{
    public interface IAccountService
    {
        Task<ActionResult<UserDto>> Register(RegisterDto registerDto);
        Task<ActionResult<UserDto>> Login(LoginDto loginDto);
    }
}