using AutoMapper;
using InfTech.Services.CatalogApi.Domain.DTOs;
using InfTech.Services.CatalogApi.Domain.Entities;
using InfTech.Services.CatalogApi.Domain.Repositories;
using InfTech.Services.CatalogApi.Domain.Services;

namespace InfTech.Services.CatalogApi.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task Add(ProductDto entity)
        {
            var product = _mapper.Map<Product>(entity);
            await _productRepository.Add(product);
        }

        public async Task Delete(int id)
        {
            var product = await _productRepository.Get(id);
            await _productRepository.Delete(product);
        }

        public async Task<ProductDto> Get(int id)
        {
            var product = await _productRepository.Get(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> Get()
        {
            var products = await _productRepository.Get();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task Update(ProductDto entity)
        {
            var product = _mapper.Map<Product>(entity);
            await _productRepository.Update(product);
        }
    }
}
