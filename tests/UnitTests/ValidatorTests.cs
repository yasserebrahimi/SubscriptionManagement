
using Xunit;
using FluentAssertions;
using SubscriptionManagement.Application.Commands;

namespace SubscriptionManagement.UnitTests
{
    public class ValidatorTests
    {
        [Fact]
        public void CreateActivate_Should_Require_UserId_And_PlanId()
        {
            var v = new CreateActivateCommandValidator();
            var result = v.Validate(new CreateActivateCommand(Guid.Empty, Guid.Empty));
            result.IsValid.Should().BeFalse();
        }
    }
}
