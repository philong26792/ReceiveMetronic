using System.Collections.Generic;
using System.Threading.Tasks;
using Receive_API.Dto;
using Receive_API.Helpers;
using Receive_API.Models;

namespace Receive_API._Services.Interfaces
{
    public interface IReceiveService
    {
        Task<List<Category>> GetAllCategory();
        Task<List<Product>> GetProductByCatID(int categoryID);
        Task<bool> ReceiveRegister(Receive_Dto model);
        Task<PagedList<ReceiveInformationModel>> GetWithPaginations(PaginationParams param, string userID);
    }
}