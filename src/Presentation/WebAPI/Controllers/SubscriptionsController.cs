
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using SubscriptionManagement.Application.Commands;
using SubscriptionManagement.Application.Common.Models;

namespace SubscriptionManagement.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriptionsController(IMediator mediator) => _mediator = mediator;

        [HttpPost("activate")]
        [ProducesResponseType(typeof(Result<SubscriptionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [SwaggerRequestExample(typeof(CreateActivateCommand), typeof(SubscriptionManagement.WebAPI.Swagger.ActivateRequestExample))]
        [SwaggerResponseExample(StatusCodes.Status409Conflict, typeof(SubscriptionManagement.WebAPI.Swagger.ConflictProblemExample))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(SubscriptionManagement.WebAPI.Swagger.NotFoundProblemExample))]
        [SwaggerResponseExample(StatusCodes.Status422UnprocessableEntity, typeof(SubscriptionManagement.WebAPI.Swagger.ValidationProblemExample))]
        public async Task<IActionResult> Activate([FromBody] CreateActivateCommand cmd, CancellationToken ct)
        {
            var res = await _mediator.Send(cmd, ct);
            if (!res.IsSuccess)
            {
                if (res.Error == "Conflict") return Conflict(new ProblemDetails { Title = "Conflict", Detail = "Active subscription exists for this plan/user" });
                if (res.Error == "NotFound") return NotFound(new ProblemDetails { Title = "Not Found", Detail = "Plan not found or inactive" });
                return BadRequest(new ProblemDetails { Title = "Bad Request", Detail = res.Error });
            }
            return Ok(res);
        }

        [HttpPut("{id:guid}/deactivate")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Deactivate(Guid id, [FromBody] DeactivateCommand body, CancellationToken ct)
        {
            var res = await _mediator.Send(new DeactivateCommand(id, body.Reason), ct);
            if (!res.IsSuccess) return NotFound(new ProblemDetails { Title = "Not Found", Detail = res.Error });
            return Ok(res);
        }
    }
}
