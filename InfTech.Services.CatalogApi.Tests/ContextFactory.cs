using InfTech.Services.CatalogApi.Domain.Entities;
using InfTech.Services.CatalogApi.Infrastructure.Repositories.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace InfTech.Services.CatalogApi.Tests
{
    public class ContextFactory
    {
        public const int GetByIdValue = 1;
        public const int UpdateByIdValue = 2;
        public const int DeleteByIdValue = 3;

        public static List<Product> Products = new List<Product>()
            {
                new Product() { ProductId = 1, Name = "Pizza", Price = 3, Image = "123" },
                new Product() { ProductId = 2, Name = "Pasta", Price = 5, Image = "345" },
                new Product() { ProductId = 3, Name = "Cola",  Price = 1, Image = "678" }
            };

        public static CatalogDbContext Create()
        {
            var options = new DbContextOptionsBuilder<CatalogDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var dbContext = new CatalogDbContext(options);
            dbContext.Products.AddRange(Products);
            dbContext.SaveChanges();

            return dbContext;
        }
    }
}
