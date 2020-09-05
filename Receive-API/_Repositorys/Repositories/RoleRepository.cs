using Receive_API._Repositorys.Interfaces;
using Receive_API.Data;
using Receive_API.Models;

namespace Receive_API._Repositorys.Repositories
{
    public class RoleRepository : ReceiveDBRepository<Role>, IRoleRepository 
    {
        private readonly DataContext _context;
        public RoleRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}