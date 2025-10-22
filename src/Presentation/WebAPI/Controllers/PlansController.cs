
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OutputCaching;
using SubscriptionManagement.Application.Common.Models;
using SubscriptionManagement.Application.Commands;
using SubscriptionManagement.Infrastructure.Persistence;

namespace SubscriptionManagement.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PlansController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public PlansController(ApplicationDbContext db) => _db = db;

        [HttpGet]
        [OutputCache(Duration = 300)]
        [ProducesResponseType(typeof(Result<List<SubscriptionPlanDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var plans = await _db.SubscriptionPlans.AsNoTracking().Where(p => p.IsActive).ToListAsync(ct);
            var dto = plans.Select(p => new SubscriptionPlanDto(p.Id, p.Name, p.Description, p.Price, p.DurationInDays)).ToList();
            return Ok(Result<List<SubscriptionPlanDto>>.Success(dto));
        }
    }
}
