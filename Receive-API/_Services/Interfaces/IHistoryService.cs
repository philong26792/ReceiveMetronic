using System.Threading.Tasks;
using Receive_API.Dto;
using Receive_API.Helpers;

namespace Receive_API._Services.Interfaces
{
    public interface IHistoryService
    {
        Task<PagedList<ReceiveInformationModel>> GetWithPaginations(PaginationParams param, string user);
        Task<PagedList<ReceiveInformationModel>> SearchWithPaginations(PaginationParams param, string userLogin, HistoryParam filterParam);
    }
}