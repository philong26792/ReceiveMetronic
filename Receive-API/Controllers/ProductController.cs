using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Receive_API._Services.Interfaces;
using Receive_API.Helpers;
using Receive_API.Models;

namespace Receive_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {   
        private readonly IProductService _serviceProduct;
        public ProductController(IProductService serviceProduct) {
            _serviceProduct = serviceProduct;
        }

        [HttpGet("getProducts")]
        public async Task<IActionResult> GetProducts([FromQuery]PaginationParams param) {
            var data = await _serviceProduct.GetWithPaginations(param);
            Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);
            return Ok(data);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]Product model) {
            var updateBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.Updated_By = updateBy;
            var result = await _serviceProduct.Add(model);
            return Ok(new {result = result});
        }

        [HttpGet("remove/{id}")]
        public async Task<IActionResult> Remove(string id) {
            var result = await _serviceProduct.Delete(id);
            return Ok(new {result = result});
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody]Product model) {
            var updateBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            model.Updated_By = updateBy;
            var result = await _serviceProduct.Update(model);
            return Ok(new {result = result});
        }

        [HttpGet("categorys")]
        public async Task<IActionResult> GetAllCategory() {
            var data = await _serviceProduct.GetAllCategory();
            return Ok(data);
        } 
    }
}