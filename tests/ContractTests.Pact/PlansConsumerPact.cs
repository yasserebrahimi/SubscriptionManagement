using PactNet;
using PactNet.Matchers;
using Xunit;
using System.Net.Http;

public class PlansConsumerPact
{
    [Fact]
    public async Task GetPlans_Contract()
    {
        using var pact = Pact.V4("SubscriptionWeb", "PlanService", new PactConfig());
        pact.WithHttpInteractions(x => x
            .UponReceiving("GET /api/v1/plans")
            .Given("plans exist")
            .WithRequest(HttpMethod.Get, "/api/v1/plans")
            .WillRespond()
            .WithStatus(200)
            .WithJsonBody(new [] { new { id = Match.Type("plan-id"), name = Match.Regex("Pro|Basic|Enterprise") } }));

        await pact.VerifyAsync(async ctx =>
        {
            using var http = new HttpClient { BaseAddress = ctx.MockServerUri };
            var res = await http.GetAsync("/api/v1/plans");
            res.EnsureSuccessStatusCode();
        });
    }
}
