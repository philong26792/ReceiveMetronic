using System.Collections.Generic;
using System.Threading.Tasks;
using Receive_API.Dto;
using Receive_API.Helpers;
using Receive_API.Models;

namespace Receive_API._Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> Add(User_Dto model);
        Task<bool> Delete(string UserId);
        Task<bool> Update(User_Dto model);
        Task<PagedList<UserViewModel>> GetWithPaginations(PaginationParams param);
        Task<List<Role>> GetAllRole();
        Task<List<Department>> GetAllDepartment();
        Task<bool> CheckExistUser(string userID);
        Task<User> GetUserById(string userId);
    }
}