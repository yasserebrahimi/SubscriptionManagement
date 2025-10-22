
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace SubscriptionManagement.IntegrationTests
{
    public class BasicApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public BasicApiTests(WebApplicationFactory<Program> factory) => _factory = factory;

        [Fact]
        public async Task Health_Should_Be_Ok()
        {
            var client = _factory.CreateClient();
            var res = await client.GetAsync("/health");
            res.EnsureSuccessStatusCode();
        }
    }
}
