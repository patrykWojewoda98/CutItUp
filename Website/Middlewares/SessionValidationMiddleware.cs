using System.IdentityModel.Tokens.Jwt;
using Website.Services;

namespace Website.Middlewares
{
    public class SessionValidationMiddleware : IMiddleware
    {
        private readonly JwtService _jwtService;

        public SessionValidationMiddleware(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var token = context.Request.Cookies["ClientToken"];
            var result = _jwtService.CheckToken(token);

            if (result.IsValid)
            {
                context.Items["TokenInfo"] = _jwtService.DecodeToken(token);
            }

            await next(context);
        }
    }

}