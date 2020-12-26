using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Contracts
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}