namespace Maxima_Tech_API.Class.Security
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuthService _authService;

        public JwtMiddleware(RequestDelegate next, AuthService authService)
        {
            _next = next;
            _authService = authService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger") || context.Request.Path.StartsWithSegments("/api/Login") )
            {
                await _next(context);
                return;
            }

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                if (_authService.ValidarToken(token))
                {
                    await _next(context);
                }
                else
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token inválido");
                }
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token não fornecido");
            }
        }
    }
}
