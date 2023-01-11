using ILogger = AuthenticationService.BLL.Services.ILogger;

namespace AuthenticationService.BLL.Middlewares
{
    public class LogMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public LogMiddleware(ILogger logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userIP = context.Connection.RemoteIpAddress;
            _logger.WriteEvent($"userIP: [{userIP}]\nЯ твой Middleware!");
            await _next(context);
        }
    }
}
