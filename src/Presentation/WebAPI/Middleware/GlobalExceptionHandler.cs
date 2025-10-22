using Microsoft.AspNetCore.Diagnostics;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace SubscriptionManagement.WebAPI.Middleware
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) => _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext http, Exception ex, CancellationToken ct)
        {
            _logger.LogError(ex, "Unhandled: {Message}", ex.Message);
            var status = ex switch
            {
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
            var problem = new ProblemDetails
            {
                Status = status,
                Title = status == 500 ? "Internal Server Error" : "Request failed",
                Detail = ex.Message,
                Instance = http.Request.Path
            };
            problem.Extensions["traceId"] = http.TraceIdentifier;
            http.Response.StatusCode = status;
            await http.Response.WriteAsJsonAsync(problem, ct);
            return true;
        }
    }
}
