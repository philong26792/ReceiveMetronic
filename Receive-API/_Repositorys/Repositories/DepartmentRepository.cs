using Receive_API._Repositorys.Interfaces;
using Receive_API.Data;
using Receive_API.Models;

namespace Receive_API._Repositorys.Repositories
{
    public class DepartmentRepository : ReceiveDBRepository<Department>, IDepartmentRepository
    {
        private readonly DataContext _context;
        public DepartmentRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}