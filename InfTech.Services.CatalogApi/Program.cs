
using InfTech.Services.CatalogApi.Infrastructure.Repositories.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace InfTech.Services.CatalogApi
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