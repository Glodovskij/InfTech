using InfTech.Services.CatalogApi.Domain.Entities;
using InfTech.Services.CatalogApi.Domain.Repositories;
using InfTech.Services.CatalogApi.Infrastructure.Repositories.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace InfTech.Services.CatalogApi.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext _catalogDbContext;
        public ProductRepository(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }
        public async Task Add(Product entity)
        {
            _catalogDbContext.Products.Add(entity);
            await _catalogDbContext.SaveChangesAsync();
        }

        public async Task Delete(Product entity)
        {
            _catalogDbContext.Products.Remove(entity);
            await _catalogDbContext.SaveChangesAsync();
        }

        public async Task<Product> Get(int id)
        {
            return await _catalogDbContext.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> Get()
        {
            return await _catalogDbContext.Products.ToListAsync();
        }

        public async Task Update(Product entity)
        {
            _catalogDbContext.Update(entity);
            await _catalogDbContext.SaveChangesAsync();
        }
    }
}
