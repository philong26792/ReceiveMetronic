using System.Threading.Tasks;
using Receive_API.Dto;
using Receive_API.Helpers;

namespace Receive_API._Services.Interfaces
{
    public interface IManagerService
    {
        Task<PagedList<ReceiveInformationModel>> GetWithPaginations(PaginationParams param);
        Task<ReceiveInformationModel> GetReceive(string receiveID);
        Task<bool> AcceptReceive(string receiveID);
        Task<bool> DecliceReceive(string receiveID);
        Task<bool> ImportExcel(string filePath, string user);
    }
}