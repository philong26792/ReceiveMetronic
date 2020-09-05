using System;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Receive_API._Services.Interfaces;
using Receive_API.Helpers;

namespace Receive_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _serviceManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ManagerController(IManagerService serviceManager, IWebHostEnvironment webHostEnvironment)
        {
            _serviceManager = serviceManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("getReceive/{id}")]
        public async Task<IActionResult> GetReceive(string id)
        {
            var data = await _serviceManager.GetReceive(id);
            return Ok(data);
        }


        [HttpGet("getReceives")]
        public async Task<IActionResult> GetReceivePagination([FromQuery] PaginationParams param)
        {
            var receives = await _serviceManager.GetWithPaginations(param);
            Response.AddPagination(receives.CurrentPage, receives.PageSize, receives.TotalCount, receives.TotalPages);
            return Ok(receives);
        }

        [HttpGet("accept/{id}")]
        public async Task<IActionResult> AcceptReceive(string id)
        {
            var result = await _serviceManager.AcceptReceive(id);
            return Ok(new { result = result });
        }

        [HttpGet("decline/{id}")]
        public async Task<IActionResult> DeclineReceive(string id)
        {
            var result = await _serviceManager.DecliceReceive(id);
            return Ok(new { result = result });
        }

        [HttpGet("importExcel")]
        public async Task<bool> ImportExcelPartment()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string filePath = "D:\\List DonVi08292020.xlsx";
            if (await _serviceManager.ImportExcel(filePath, user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}