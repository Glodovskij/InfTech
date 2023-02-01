using InfTech.Services.CatalogApi.Core.Controllers;
using InfTech.Services.CatalogApi.Domain.DTOs;
using InfTech.Services.CatalogApi.Domain.Messaging;
using InfTech.Services.CatalogApi.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace InfTech.Services.CatalogApi.Tests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly List<ProductDto> _products;
        public ProductControllerTests()
        {
            _products = new List<ProductDto>
            {
                new ProductDto { ProductId = 1, Name = "Pizza", Price = 3, Image = "123" },
                new ProductDto { ProductId = 2, Name = "Pasta", Price = 5, Image = "345" },
                new ProductDto { ProductId = 3, Name = "Cola",  Price = 1, Image = "678" }
            };

            _productServiceMock = new Mock<IProductService>();
            _productServiceMock.Setup(x => x.Get(ContextFactory.GetByIdValue)).ReturnsAsync(_products
                .Find(x => x.ProductId == ContextFactory.GetByIdValue));
            _productServiceMock.Setup(x => x.Get()).ReturnsAsync(_products);
        }
        [Fact]
        public async Task GetAll_ReturnsCorrectDtoList()
        {
            // Arrange 
            var controller = new ProductController(_productServiceMock.Object, null);

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var returnedData = (result as OkObjectResult).Value as List<ProductDto>;
            Assert.Equal(_products, returnedData);
        }

        [Fact]
        public async Task GetAll_ReturnsNotFoundIfDatabaseIsEmpty()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.Get()).ReturnsAsync(new List<ProductDto>());
            var controller = new ProductController(productServiceMock.Object, null);

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Get_RetunsCorrectProductDtoById()
        {
            // Arrange
            var controller = new ProductController(_productServiceMock.Object, null);

            // Act
            var result = await controller.Get(ContextFactory.GetByIdValue);

            // Assert

            Assert.IsType<OkObjectResult>(result);
            var returnedData = (result as OkObjectResult).Value as ProductDto;
            Assert.Equal(_products.Find(x => x.ProductId == ContextFactory.GetByIdValue), returnedData);
        }

        [Fact]
        public async Task GetAll_ReturnsNotFoundIfNonExistantKeyGiven()
        {
            // Arrange
            var controller = new ProductController(_productServiceMock.Object, null);

            // Act
            var result = await controller.Get(101);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Add_InvokesServicesMethodsAddAndSendMessageAndReturnsOk()
        {
            // Arrange
            var testProduct = _products.First();
            var rabbitMqServiceMock = new Mock<IRabbitMqService>();
            rabbitMqServiceMock.Setup(x => x.SendMessage(It.IsAny<RmqMessage>()));
            var controller = new ProductController(_productServiceMock.Object, rabbitMqServiceMock.Object);


            // Act
            var result = await controller.Add(testProduct);

            // Assert
            Assert.IsType<OkResult>(result);
            _productServiceMock.Verify(x => x.Add(testProduct), Times.Once());
            rabbitMqServiceMock.Verify(x => x.SendMessage(It.IsAny<RmqMessage>()), Times.Once());
        }

        [Fact]
        public async Task Delete_InvokesServicesDeleteMethodAndReturnsOk()
        {
            // Arrang
            var controller = new ProductController(_productServiceMock.Object, null);

            // Act
            var result = await controller.Delete(ContextFactory.DeleteByIdValue);

            // Assert
            Assert.IsType<OkResult>(result);
            _productServiceMock.Verify(x => x.Delete(It.IsAny<int>()));
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundForNonExistentId()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.Delete(It.IsAny<int>()))
                .Throws(new ArgumentNullException());

            var controller = new ProductController(productServiceMock.Object, null);

            // Act
            var result = await controller.Delete(1001);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_InvokesServicesUpdateMethodAndReturnsOk()
        {
            // Arrange
            var testProduct = _products.First();
            testProduct.Name = "Burger";
            var controller = new ProductController(_productServiceMock.Object, null);

            // Act
            var result = await controller.Update(testProduct);

            // Assert
            Assert.IsType<OkResult>(result);
            _productServiceMock.Verify(x => x.Update(It.IsAny<ProductDto>()), Times.Once);
        }

        [Fact]
        public async Task Update_ReturnsNotFoundForNonExistentId()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.Update(It.IsAny<ProductDto>()))
                .Throws(new ArgumentNullException());

            var controller = new ProductController(productServiceMock.Object, null);

            // Act
            var result = await controller.Update(new ProductDto());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


    }
}
