using Microsoft.AspNetCore.Hosting.Server;
using Serilog;
using System.Net;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookingRoom.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[Exception] {Message}", ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var error = new
                {
                    status = context.Response.StatusCode,
                    message = "Internal Server Error",
                    detail = ex.Message
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(error));
            }
        }
    }
}
