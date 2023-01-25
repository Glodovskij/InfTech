using InfTech.Services.CatalogApi.Domain.DTOs;
using InfTech.Services.CatalogApi.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfTech.Services.CatalogApi.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.Get());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productService.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductDto product)
        {
            await _productService.Add(product);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductDto product)
        {
            await _productService.Update(product);
            return Ok();
        }

    }
}
