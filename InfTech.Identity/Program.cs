using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "InfTech")))
    .SetApplicationName("IdentityApp");

builder.Services.AddAuthorization();

builder.Services.AddAuthentication()
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", (HttpContext ctx) =>
{
    ctx.SignInAsync(new ClaimsPrincipal(new[]
    {
        new ClaimsIdentity(new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, "InfTech")
        }, CookieAuthenticationDefaults.AuthenticationScheme)
    }));
    return "ok";
});

app.Run();