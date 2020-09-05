using Receive_API._Repositorys.Interfaces;
using Receive_API.Data;
using Receive_API.Models;

namespace Receive_API._Repositorys.Repositories
{
    public class ProductRepository : ReceiveDBRepository<Product>, IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}