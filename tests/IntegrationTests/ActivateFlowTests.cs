
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using SubscriptionManagement.Application.Commands;
using Xunit;

namespace SubscriptionManagement.IntegrationTests
{
    public class ActivateFlowTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public ActivateFlowTests(WebApplicationFactory<Program> factory) => _factory = factory;

        [Fact]
        public async Task Activate_Without_IdempotencyKey_Should_400()
        {
            var client = _factory.CreateClient();
            var res = await client.PostAsJsonAsync("/api/v1/subscriptions/activate",
                new CreateActivateCommand(Guid.NewGuid(), Guid.NewGuid()));
            Assert.Equal(HttpStatusCode.BadRequest, res.StatusCode);
        }
    }
}
