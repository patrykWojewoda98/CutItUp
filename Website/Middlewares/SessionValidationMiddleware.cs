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
            var token = context.Session.GetString("ClientToken");
            var tokenCheck = _jwtService.CheckToken(token);

            context.Items["TokenInfo"] = tokenCheck.IsValid
                ? _jwtService.DecodeToken(token)
                : tokenCheck;

            await next(context);
        }
    }

}