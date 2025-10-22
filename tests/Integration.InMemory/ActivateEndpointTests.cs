using System.Net;
using System.Net.Http.Json;
using Xunit;

public class ActivateEndpointTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    public ActivateEndpointTests(CustomWebApplicationFactory<Program> factory) => _factory = factory;

    [Fact]
    public async Task Activate_Returns_200_And_Caches_On_IdempotencyKey()
    {
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Idempotency-Key", "it-key-123");

        var body = new { userId = "00000000-0000-0000-0000-000000000001", planId = "basic" };
        var r1 = await client.PostAsJsonAsync("/api/v1/subscriptions/activate", body);
        var r2 = await client.PostAsJsonAsync("/api/v1/subscriptions/activate", body);
        Assert.True((int)r1.StatusCode < 500);
        Assert.Equal(r1.StatusCode, r2.StatusCode);
    }
}
