using PactNet.Verifier;
using Xunit;
using System.IO;

public class ProviderVerification
{
    [Fact]
    public void Verify_Pacts_If_Exist()
    {
        var pactPath = Path.Combine("pacts", "SubscriptionWeb-PlanService.json");
        if (!File.Exists(pactPath))
        {
            // Skip if no pact file exists in repo
            return;
        }

        var verifier = new PactVerifier(new PactVerifierConfig
        {
            Outputters = new[] { new XUnitOutput(new TestOutputHelper()) }
        });

        verifier.ServiceProvider("PlanService", new Uri("http://localhost:5080"))
                .WithFileSource(new FileInfo(pactPath))
                .Verify();
    }
}

class XUnitOutput : PactNet.Infrastructure.Outputters.IOutput
{
    private readonly Xunit.Abstractions.ITestOutputHelper _output;
    public XUnitOutput(Xunit.Abstractions.ITestOutputHelper output) => _output = output;
    public void WriteLine(string line) => _output.WriteLine(line);
}
