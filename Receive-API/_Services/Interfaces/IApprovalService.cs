using System.Threading.Tasks;
using Receive_API.Dto;
using Receive_API.Helpers;

namespace Receive_API._Services.Interfaces
{
    public interface IApprovalService
    {
        Task<PagedList<ReceiveInformationModel>> GetWithPaginations(PaginationParams param, string userID);
        Task<PagedList<ReceiveInformationModel>> SearchWithPaginations(PaginationParams param, string userReceive, string userAccept);
        Task<bool> AcceptReceive(string receiveID, string userID);
        Task<bool> DeclineReceive(string receiveID, string userID);
    }
}