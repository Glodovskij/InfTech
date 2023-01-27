using InfTech.Services.EmailApi.Configuration;
using InfTech.Services.EmailApi.Services;

namespace InfTech.Services.EmailApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("Configuration\\SmtpConfig.Json", optional: false, reloadOnChange: true);
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<SmtpConfigOptions>(builder.Configuration.GetSection(SmtpConfigOptions.SmtpConfiguration));
            builder.Services.AddTransient<ISmtpClient, SmtpClient>();

            builder.Services.AddHostedService<RabbitMqService>();

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