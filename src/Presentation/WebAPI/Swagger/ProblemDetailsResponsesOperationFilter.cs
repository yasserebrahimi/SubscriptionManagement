using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SubscriptionManagement.WebAPI.Swagger;

public class ProblemDetailsResponsesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        AddProblem(operation, "400", ProblemDetailsExampleFactory.ValidationProblem());
        AddProblem(operation, "401", ProblemDetailsExampleFactory.AuthProblem());
        AddProblem(operation, "404", ProblemDetailsExampleFactory.NotFoundProblem());
        AddProblem(operation, "409", ProblemDetailsExampleFactory.ConflictProblem());
        AddProblem(operation, "429", ProblemDetailsExampleFactory.RateLimitProblem());
    }

    private static void AddProblem(OpenApiOperation op, string statusCode, OpenApiObject example)
    {
        if (!op.Responses.TryGetValue(statusCode, out var resp))
        {
            resp = new OpenApiResponse { Description = "Error" };
            op.Responses[statusCode] = resp;
        }

        resp.Content ??= new Dictionary<string, OpenApiMediaType>();
        var mt = resp.Content.TryGetValue("application/problem+json", out var existing)
            ? existing
            : (resp.Content["application/problem+json"] = new OpenApiMediaType());

        mt.Schema = new OpenApiSchema { Type = "object", AdditionalPropertiesAllowed = true };
        mt.Examples ??= new Dictionary<string, OpenApiExample>();
        mt.Examples["example"] = new OpenApiExample { Value = example };
    }
}
