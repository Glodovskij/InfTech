using InfTech.Services.CatalogApi.Domain.Entities;
using InfTech.Services.CatalogApi.Infrastructure.Repositories;
using InfTech.Services.CatalogApi.Infrastructure.Repositories.Data.Configuration;
using Xunit;

namespace InfTech.Services.CatalogApi.Tests
{
    public class ProductRepositoryTests : IDisposable
    {
        private readonly CatalogDbContext _dbContext;

        public ProductRepositoryTests()
        {
            _dbContext = ContextFactory.Create();
        }

        [Fact]
        public async Task Get_ShouldReturnProductByGivenId()
        {
            // Arrange
            var repository = new ProductRepository(_dbContext);

            // Act
            var result = await repository.Get(ContextFactory.GetByIdValue);

            // Assert 
            Assert.Equal(ContextFactory.Products.Find(p => p.ProductId == ContextFactory.GetByIdValue), result);
        }

        [Fact]
        public async Task Get_ShouldReturnAllProducts()
        {
            // Arrange
            var repository = new ProductRepository(_dbContext);

            // Act
            var result = await repository.Get();

            // Assert
            Assert.Equal(ContextFactory.Products.Count, result.Count());
            foreach (var expectedProduct in ContextFactory.Products)
            {
                var actualProduct = result.Single(x => x.ProductId == expectedProduct.ProductId);
                Assert.Equal(expectedProduct.ProductId, actualProduct.ProductId);
                Assert.Equal(expectedProduct.Name, actualProduct.Name);
                Assert.Equal(expectedProduct.Price, actualProduct.Price);
                Assert.Equal(expectedProduct.Image, actualProduct.Image);
            }

        }

        [Fact]
        public async Task Delete_RemovesProductFromDb()
        {
            // Arrange
            var repository = new ProductRepository(_dbContext);

            // Act
            await repository.Delete(await repository.Get(ContextFactory.DeleteByIdValue));

            // Assert
            Assert.Null(await repository.Get(ContextFactory.DeleteByIdValue));
        }

        [Fact]
        public async Task Add_ProductAddedToDb()
        {
            // Arrange
            var repository = new ProductRepository(_dbContext);

            var product = new Product
            {
                ProductId = 4,
                Name = "Burger",
                Price = 6,
                Image = ""
            };

            // Act
            await repository.Add(product);

            // Assert
            Assert.Equal(product, await repository.Get(product.ProductId));
        }

        [Fact]
        public async Task Update_UpdatedEntityChangedValueInDb()
        {
            // Arrange
            var repository = new ProductRepository(_dbContext);

            // Act
            var productToEdit = await repository.Get(ContextFactory.UpdateByIdValue);
            productToEdit.Name = "Pizza with pepperoni";
            productToEdit.Price = 6;

            await repository.Update(productToEdit);

            // Assert
            Assert.Equal(productToEdit, await repository.Get(ContextFactory.UpdateByIdValue));
        }

        [Fact]
        public async Task Update_ThrowsNullExceptionWhenTargetObjectNotFound()
        {
            // Arrange
            var repository = new ProductRepository(_dbContext);
            var productToEdit = new Product()
            {
                ProductId = 22,
                Name = "Pizza with pepperoni",
                Price = 6,
                Image = ""
            };

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.Update(productToEdit));
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
