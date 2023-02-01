using InfTech.Services.CatalogApi.Domain.DTOs;
using InfTech.Services.CatalogApi.Domain.Messaging;
using InfTech.Services.CatalogApi.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InfTech.Services.CatalogApi.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IRabbitMqService _rabbitMqService;
        public ProductController(IProductService productService, IRabbitMqService rabbitMqService)
        {
            _productService = productService;
            _rabbitMqService = rabbitMqService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.Get();
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _productService.Get(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductDto product)
        {
            await _productService.Add(product);
            _rabbitMqService.SendMessage(new RmqMessage() { QueueName ="InfTech-email", Message = $"New product added! {product.Name}" });
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productService.Delete(id);
            }
            catch (ArgumentNullException) 
            { 
                return NotFound();
            }
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductDto product)
        {
            try
            {
                await _productService.Update(product);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
