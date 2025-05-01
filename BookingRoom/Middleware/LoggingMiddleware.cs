using System.Diagnostics;

namespace BookingRoom.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            var method = context.Request.Method;
            var path = context.Request.Path;

            _logger.LogInformation($"[Request] {method} {path}");

            await _next(context);

            stopwatch.Stop();

            var statusCode = context.Response.StatusCode;
            var duration = stopwatch.ElapsedMilliseconds;

            _logger.LogInformation($"[Response] {method} {path} responded {statusCode} in {duration} ms");
        }
    }
}
