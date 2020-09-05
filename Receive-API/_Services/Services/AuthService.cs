using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Receive_API._Repositorys.Interfaces;
using Receive_API._Services.Interfaces;
using Receive_API.Dto;
using Receive_API.Models;

namespace Receive_API._Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repoUser;
        public AuthService(IUserRepository repoUser) {
            _repoUser = repoUser;
        }
        public async Task<User> Login(UserForLoginDto userDto)
        {
            var user = await _repoUser.GetAll()
            .Where(x => x.ID.Trim() == userDto.Username.Trim() &&
                    x.Password.Trim() == userDto.Password.Trim()).FirstOrDefaultAsync();
            return user;
        }
    }
}