using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Receive_API._Services.Interfaces;
using Receive_API.Dto;
using Receive_API.Helpers;

namespace Receive_API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _serviceUser;
        public UserController(IUserService serviceUser) {
            _serviceUser = serviceUser;
        }

        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUsers([FromQuery]PaginationParams param) {
            var users = await _serviceUser.GetWithPaginations(param);
            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(users);
        }

        [HttpGet("getUser/{userID}")]
        public async Task<IActionResult> GetUserByID(string userID) {
            var user = await _serviceUser.GetUserById(userID);
            return Ok(user);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddUser([FromBody]User_Dto model) {
            var updateBy = User.FindFirst(ClaimTypes.Name).Value;
            model.Update_By = updateBy;
            var result = await _serviceUser.Add(model);
            return Ok(new {result = result});
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id) {
            var result = await _serviceUser.Delete(id);
            return Ok(result);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> EditUser([FromBody]User_Dto model) {
            var updateBy = User.FindFirst(ClaimTypes.Name).Value;
            model.Update_By = updateBy;
            var result = await _serviceUser.Update(model);
            return Ok(new {result = result});
        }

        [HttpGet("getRoles")]
        public async Task<IActionResult> GetAllRole() {
            var result = await _serviceUser.GetAllRole();
            return Ok(result);
        }

        [HttpGet("getDepartments")]
        public async Task<IActionResult> GetAllDepartment() {
            var result = await _serviceUser.GetAllDepartment();
            return Ok(result);
        }
    }
}