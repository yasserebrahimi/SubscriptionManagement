
using Xunit;
using FluentAssertions;
using SubscriptionManagement.Domain;

namespace SubscriptionManagement.UnitTests
{
    public class DomainTests
    {
        [Fact]
        public void Create_Subscription_Should_Set_Active_And_EndDate()
        {
            var sub = Subscription.Create(Guid.NewGuid(), Guid.NewGuid(), 30);
            sub.Status.Should().Be(SubscriptionStatus.Active);
            sub.EndDate.Should().NotBeNull();
        }
    }
}
