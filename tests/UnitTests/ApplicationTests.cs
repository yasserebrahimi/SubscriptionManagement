
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.Application.Commands;
using SubscriptionManagement.Domain;
using SubscriptionManagement.Infrastructure.Persistence;
using Xunit;

namespace SubscriptionManagement.UnitTests
{
    public class ApplicationTests
    {
        private ApplicationDbContext MakeDb()
        {
            var opts = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            return new ApplicationDbContext(opts);
        }

        [Fact]
        public async Task Activate_Should_Return_NotFound_When_Plan_Missing()
        {
            using var db = MakeDb();
            var handler = new CreateActivateHandler(db);
            var result = await handler.Handle(new CreateActivateCommand(Guid.NewGuid(), Guid.NewGuid()), CancellationToken.None);
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("NotFound");
        }

        [Fact]
        public async Task Activate_Should_Return_Conflict_When_Active_Exists_PerPlan()
        {
            using var db = MakeDb();
            var plan = new SubscriptionPlan("Pro","Pro",19.99m,30);
            db.SubscriptionPlans.Add(plan);
            var userId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var existing = Subscription.Create(userId, plan.Id, 30);
            db.Subscriptions.Add(existing);
            await db.SaveChangesAsync();

            var handler = new CreateActivateHandler(db);
            var result = await handler.Handle(new CreateActivateCommand(userId, plan.Id), CancellationToken.None);
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Conflict");
        }
    }
}
