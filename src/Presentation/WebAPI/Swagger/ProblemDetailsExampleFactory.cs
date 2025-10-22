using Microsoft.OpenApi.Any;

namespace SubscriptionManagement.WebAPI.Swagger;

public static class ProblemDetailsExampleFactory
{
    public static OpenApiObject ValidationProblem() => new()
    {
        ["type"] = new OpenApiString("https://httpstatuses.io/400"),
        ["title"] = new OpenApiString("One or more validation errors occurred."),
        ["status"] = new OpenApiInteger(400),
        ["traceId"] = new OpenApiString("|trace-id"),
        ["errors"] = new OpenApiObject
        {
            ["planId"] = new OpenApiArray { new OpenApiString("The planId format is invalid.") }
        }
    };

    public static OpenApiObject AuthProblem() => new()
    {
        ["type"] = new OpenApiString("https://httpstatuses.io/401"),
        ["title"] = new OpenApiString("Unauthorized"),
        ["status"] = new OpenApiInteger(401),
        ["detail"] = new OpenApiString("Bearer token is missing or invalid.")
    };

    public static OpenApiObject NotFoundProblem() => new()
    {
        ["type"] = new OpenApiString("https://httpstatuses.io/404"),
        ["title"] = new OpenApiString("Not Found"),
        ["status"] = new OpenApiInteger(404),
        ["detail"] = new OpenApiString("Subscription was not found.")
    };

    public static OpenApiObject ConflictProblem() => new()
    {
        ["type"] = new OpenApiString("https://httpstatuses.io/409"),
        ["title"] = new OpenApiString("Conflict"),
        ["status"] = new OpenApiInteger(409),
        ["detail"] = new OpenApiString("User already has an Active subscription.")
    };

    public static OpenApiObject RateLimitProblem() => new()
    {
        ["type"] = new OpenApiString("https://httpstatuses.io/429"),
        ["title"] = new OpenApiString("Too Many Requests"),
        ["status"] = new OpenApiInteger(429),
        ["detail"] = new OpenApiString("Rate limit exceeded. Retry later."),
        ["extensions"] = new OpenApiObject
        {
            ["limit"] = new OpenApiInteger(200),
            ["remaining"] = new OpenApiInteger(0),
            ["reset"] = new OpenApiString("2025-10-22T10:15:00Z")
        }
    };
}
