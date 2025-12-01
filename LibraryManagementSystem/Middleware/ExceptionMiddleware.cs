using LibraryManagementSystem.Exceptions;
using System.Net;
using System.Text.Json;

namespace LibraryManagementSystem.Middleware
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            string message = ex.Message;

            switch (ex)
            {
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound; // 404
                    break;
                case BadRequestException:
                    statusCode = HttpStatusCode.BadRequest; // 400
                    break;
                case UnauthorizedException:
                    statusCode = HttpStatusCode.Unauthorized; // 401
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError; // 500
                    message = "An unexpected error occurred.";
                    break;
            }

            response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new
            {
                StatusCode = response.StatusCode,
                Message = message
            });

            await response.WriteAsync(result);
        }
    }

    
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
