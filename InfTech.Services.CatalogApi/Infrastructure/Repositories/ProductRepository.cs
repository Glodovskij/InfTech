using InfTech.Services.CatalogApi.Domain.Entities;
using InfTech.Services.CatalogApi.Domain.Repositories;
using InfTech.Services.CatalogApi.Infrastructure.Repositories.Data.Configuration;

namespace InfTech.Services.CatalogApi.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext _catalogDbContext;
        public ProductRepository(CatalogDbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }
        public void Add(Product entity)
        {
            _catalogDbContext.Products.Add(entity);
            _catalogDbContext.SaveChanges();
        }

        public void Delete(Product entity)
        {
            _catalogDbContext.Products.Remove(entity);
            _catalogDbContext.SaveChanges();
        }

        public Product Get(int id)
        {
            return _catalogDbContext.Products.Find(id);
        }

        public List<Product> Get()
        {
            return _catalogDbContext.Products.ToList();
        }

        public void Update(Product entity)
        {
            _catalogDbContext.Update(entity);
            _catalogDbContext.SaveChanges();
        }
    }
}
