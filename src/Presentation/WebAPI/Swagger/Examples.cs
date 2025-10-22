
using Microsoft.AspNetCore.Mvc;
using SubscriptionManagement.Application.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace SubscriptionManagement.WebAPI.Swagger
{
    public class ActivateRequestExample : IExamplesProvider<CreateActivateCommand>
    {
        public CreateActivateCommand GetExamples() =>
            new CreateActivateCommand(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));
    }

    public class ConflictProblemExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples() => new ProblemDetails
        {
            Title = "Conflict",
            Status = StatusCodes.Status409Conflict,
            Detail = "Active subscription exists for this plan/user"
        };
    }

    public class NotFoundProblemExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples() => new ProblemDetails
        {
            Title = "Not Found",
            Status = StatusCodes.Status404NotFound,
            Detail = "Plan not found or inactive"
        };
    }

    public class ValidationProblemExample : IExamplesProvider<ValidationProblemDetails>
    {
        public ValidationProblemDetails GetExamples() =>
            new ValidationProblemDetails(new Dictionary<string, string[]> {
                ["UserId"] = new [] { "UserId must not be empty." },
                ["PlanId"] = new [] { "PlanId must not be empty." }
            })
            {
                Title = "Validation Failed",
                Status = StatusCodes.Status422UnprocessableEntity
            };
    }
}
