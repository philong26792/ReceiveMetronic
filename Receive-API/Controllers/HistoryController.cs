using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Receive_API._Services.Interfaces;
using Receive_API.Dto;
using Receive_API.Helpers;

namespace Receive_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _service;
        public HistoryController(IHistoryService service) {
            _service = service;
        }
        
        [HttpGet("getHistorys")]
        public async Task<IActionResult> GetHistorys([FromQuery]PaginationParams param) {
            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var data = await _service.GetWithPaginations(param, user);
            Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);
            return Ok(data);
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchHistory([FromQuery]PaginationParams param, HistoryParam filterParam) {
            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var data = await _service.SearchWithPaginations(param, user, filterParam);
            Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);
            return Ok(data);
        }
    }
}