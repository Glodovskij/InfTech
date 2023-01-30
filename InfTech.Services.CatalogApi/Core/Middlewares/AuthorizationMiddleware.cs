namespace InfTech.Services.CatalogApi.Core.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(context.User.Identity?.Name != "InfTech")
            {
                context.Response.StatusCode = 401;
                return;
            }

            await _next(context);
        }
    }
}
