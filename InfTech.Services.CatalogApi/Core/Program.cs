using InfTech.Services.CatalogApi.Domain.Messaging;
using InfTech.Services.CatalogApi.Domain.Repositories;
using InfTech.Services.CatalogApi.Domain.Services;
using InfTech.Services.CatalogApi.Infrastructure.Configuration;
using InfTech.Services.CatalogApi.Infrastructure.Messaging;
using InfTech.Services.CatalogApi.Infrastructure.Repositories;
using InfTech.Services.CatalogApi.Infrastructure.Repositories.Data.Configuration;
using InfTech.Services.CatalogApi.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace InfTech.Services.CatalogApi.Core
{
    public class Program
    {
        private const string ConnectionStringName = "CatalogConnectionString";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<CatalogDbContext>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionStringName)));

            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<CatalogMapperProfile>(), AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<IProductService, ProductService>();

            builder.Services.AddTransient<IRabbitMqService, RabbitMqService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}