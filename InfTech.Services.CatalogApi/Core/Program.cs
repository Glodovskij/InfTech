using InfTech.Services.CatalogApi.Core.Middlewares;
using InfTech.Services.CatalogApi.Domain.Messaging;
using InfTech.Services.CatalogApi.Domain.Repositories;
using InfTech.Services.CatalogApi.Domain.Services;
using InfTech.Services.CatalogApi.Infrastructure.Configuration;
using InfTech.Services.CatalogApi.Infrastructure.Messaging;
using InfTech.Services.CatalogApi.Infrastructure.Repositories;
using InfTech.Services.CatalogApi.Infrastructure.Repositories.Data.Configuration;
using InfTech.Services.CatalogApi.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace InfTech.Services.CatalogApi.Core
{
    public class Program
    {
        private const string ConnectionStringName = "CatalogConnectionString";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "InfTech")))
                .SetApplicationName("IdentityApp");

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication()
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<CatalogDbContext>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionStringName)));

            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<CatalogMapperProfile>(), AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<IProductService, ProductService>();

            builder.Services.AddTransient<IRabbitMqService, RabbitMqService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseMiddleware<AuthorizationMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}