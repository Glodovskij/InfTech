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

            builder.Services.Configure<SmtpConfigOptions>(builder.Configuration.GetSection(SmtpConfigOptions.SmtpConfiguration));
            builder.Services.AddTransient<ISmtpClient, SmtpClient>();

            builder.Services.AddHostedService<RabbitMqService>();

            var app = builder.Build();

            app.Run();
        }
    }
}