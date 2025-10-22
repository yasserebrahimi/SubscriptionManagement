
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FluentValidation;

namespace SubscriptionManagement.WebAPI.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException vex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                var details = new ValidationProblemDetails(
                    vex.Errors
                       .GroupBy(e => e.PropertyName)
                       .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray())
                )
                {
                    Title = "Validation Failed",
                    Status = StatusCodes.Status422UnprocessableEntity
                };
                await context.Response.WriteAsJsonAsync(details);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    context.Response.StatusCode,
                    Message = "An unexpected error occurred."
                }));
            }
        }
    }
}
