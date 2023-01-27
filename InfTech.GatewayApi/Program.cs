using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace InfTech.GatewayApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddOcelot();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            await app.UseOcelot();

            app.Run();
        }
    }
}