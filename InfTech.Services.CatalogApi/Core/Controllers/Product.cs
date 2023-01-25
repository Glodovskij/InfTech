using InfTech.Services.CatalogApi.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfTech.Services.CatalogApi.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Product : ControllerBase
    {
        private readonly IProductService _productService;
        public Product(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_productService.Get());
        }
    }
}
