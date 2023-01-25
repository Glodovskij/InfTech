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
        public void Add(ProductDto entity)
        {
            var product = _mapper.Map<Product>(entity);
            _productRepository.Add(product);
        }

        public void Delete(int id)
        {
            var product = _productRepository.Get(id);
            _productRepository.Delete(product);
        }

        public ProductDto Get(int id)
        {
            var product = _productRepository.Get(id);
            return _mapper.Map<ProductDto>(product);
        }

        public List<ProductDto> Get()
        {
            var products = _productRepository.Get();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public void Update(ProductDto entity)
        {
            var product = _mapper.Map<Product>(entity);
            _productRepository.Update(product);
        }
    }
}
