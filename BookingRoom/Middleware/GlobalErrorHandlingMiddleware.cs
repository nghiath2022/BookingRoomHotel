using FluentValidation;
using System.Net;
using System.Text.Json;

namespace BookingRoom.Middleware
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // gọi middleware tiếp theo
            }
            catch (ValidationException validationEx)
            {
                _logger.LogWarning("Validation failed: {Message}", validationEx.Message);
                await HandleValidationExceptionAsync(context, validationEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                await HandleGeneralExceptionAsync(context, ex);
            }
        }

        private static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new { field = e.PropertyName, message = e.ErrorMessage });

            var response = new
            {
                statusCode = (int)HttpStatusCode.BadRequest,
                message = "Validation failed.",
                errors
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static async Task HandleGeneralExceptionAsync(HttpContext context, Exception ex)
        {
            var response = new
            {
                statusCode = (int)HttpStatusCode.InternalServerError,
                message = "An unexpected error occurred.",
                detail = ex.Message // ⛔ ẩn đi nếu ở môi trường production
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
