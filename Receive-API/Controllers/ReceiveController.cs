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
    public class ReceiveController : ControllerBase
    {
        private readonly IReceiveService _serviceReceive;
        private readonly IUserService _serverUser;
        public ReceiveController(IReceiveService serviceReceive,
                                IUserService serverUser) {
            _serviceReceive = serviceReceive;
            _serverUser = serverUser;
        }

        [HttpGet("getCategorys")]
        public async Task<IActionResult> GetAllCategory() {
            var data = await _serviceReceive.GetAllCategory();
            return Ok(data);
        }

        [HttpGet("getProducts/{id}")]
        public async Task<IActionResult> GetProductByCatID(int id) {
            var data = await _serviceReceive.GetProductByCatID(id);
            return Ok(data);
        }

        [HttpPost("receiveRegister")]
        public async Task<IActionResult> ReceiveRegister([FromBody]Receive_Dto model) {
            var userCurrent = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.UserID = userCurrent;
            var user = await _serverUser.GetUserById(userCurrent);
            model.DepID =  user.DepID;
            var result = await _serviceReceive.ReceiveRegister(model);
            return Ok(new {result = result});
        }

        [HttpGet("getReceives")]
        public async Task<IActionResult> GetReceivePagination([FromQuery]PaginationParams param) {
            var userCurrent = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var receives = await _serviceReceive.GetWithPaginations(param, userCurrent);
            Response.AddPagination(receives.CurrentPage, receives.PageSize, receives.TotalCount, receives.TotalPages);
            return Ok(receives);
        }
        
    }
}