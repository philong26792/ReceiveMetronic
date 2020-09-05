using System.Threading.Tasks;
using Receive_API.Dto;
using Receive_API.Models;

namespace Receive_API._Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> Login(UserForLoginDto userDto);
    }
}