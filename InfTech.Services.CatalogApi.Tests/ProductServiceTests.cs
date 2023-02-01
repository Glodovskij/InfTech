using AutoMapper;
using InfTech.Services.CatalogApi.Domain.DTOs;
using InfTech.Services.CatalogApi.Domain.Entities;
using InfTech.Services.CatalogApi.Domain.Repositories;
using InfTech.Services.CatalogApi.Infrastructure.Configuration;
using InfTech.Services.CatalogApi.Infrastructure.Services;
using Moq;
using Xunit;

namespace InfTech.Services.CatalogApi.Tests
{
    public class ProductServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProductRepository> _repositoryMock;

        public ProductServiceTests()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new CatalogMapperProfile()));
            _mapper = mapperConfig.CreateMapper();

            _repositoryMock = new Mock<IProductRepository>();
            _repositoryMock.Setup(x => x.Get(ContextFactory.GetByIdValue)).ReturnsAsync(ContextFactory.Products
                .Find(x => x.ProductId == ContextFactory.GetByIdValue));
            _repositoryMock.Setup(x => x.Get(ContextFactory.UpdateByIdValue)).ReturnsAsync(ContextFactory.Products
                .Find(x => x.ProductId == ContextFactory.UpdateByIdValue));
            _repositoryMock.Setup(x => x.Get()).ReturnsAsync(ContextFactory.Products);
        }
        [Fact]
        public async Task Get_ReturnsCorrectProductDtoById()
        {
            // Arrange
            var productService = new ProductService(_repositoryMock.Object, _mapper);

            // Act
            var result = await productService.Get(ContextFactory.GetByIdValue);

            // Assert
            _repositoryMock.Verify(x => x.Get(ContextFactory.GetByIdValue), Times.Once());
            Assert.IsType<ProductDto>(result);
        }

        [Fact]
        public async Task Get_ReturnsCorrectProductDtoList()
        {
            // Arrange
            var productService = new ProductService(_repositoryMock.Object, _mapper);

            // Act
            var result = await productService.Get();

            // Assert
            _repositoryMock.Verify(x => x.Get(), Times.Once());
            Assert.IsAssignableFrom<IEnumerable<ProductDto>>(result);
            Assert.Equal(ContextFactory.Products.Count, result.Count());
        }

        [Fact]
        public async Task Delete_InvokesRepositoryMethodDelete()
        {
            // Arrange
            var productService = new ProductService(_repositoryMock.Object, _mapper);

            // Act
            await productService.Update(new ProductDto());

            // Assert
            _repositoryMock.Verify(x => x.Update(It.IsAny<Product>()), Times.Once());
        }

        [Fact]
        public async Task Update_InvokesRepositoryMethodUpdate()
        {
            // Arrange
            var productService = new ProductService(_repositoryMock.Object, _mapper);

            // Act
            var productToUpdate =  await productService.Get(ContextFactory.UpdateByIdValue);
            productToUpdate.Name = "Pizza with pepperoni";

            await productService.Update(productToUpdate);

            // Assert
            _repositoryMock.Verify(x => x.Update(It.IsAny<Product>()), Times.Once());
        }

        [Fact]
        public async Task Add_InvokesRepositoryMethodAdd()
        {
            // Arrange
            var productService = new ProductService(_repositoryMock.Object, _mapper);

            // Act
            await productService.Add(new ProductDto());

            //
            _repositoryMock.Verify(x => x.Add(It.IsAny<Product>()), Times.Once());
        }
    }
}
