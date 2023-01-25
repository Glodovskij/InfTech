using InfTech.Services.CatalogApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfTech.Services.CatalogApi.Infrastructure.Repositories.Data.Configuration
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; }
    }
}
