using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Npgsql;
using Xunit;

public class PostgresIntegrationTests : IAsyncLifetime
{
    private readonly TestcontainersContainer _pg = new TestcontainersBuilder<TestcontainersContainer>()
        .WithImage("postgres:16-alpine")
        .WithEnvironment("POSTGRES_DB", "subscriptiondb")
        .WithEnvironment("POSTGRES_PASSWORD", "dev")
        .WithPortBinding(5432, true)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
        .Build();

    private string _conn = default!;

    public async Task InitializeAsync()
    {
        await _pg.StartAsync();
        var hostPort = _pg.GetMappedPublicPort(5432);
        _conn = $"Host=localhost;Port={hostPort};Database=subscriptiondb;Username=postgres;Password=dev";
        await using var c = new NpgsqlConnection(_conn);
        await c.OpenAsync();
        var r = await new NpgsqlCommand("select 1", c).ExecuteScalarAsync();
        Assert.Equal(1, r);
    }

    public Task DisposeAsync() => _pg.StopAsync();

    [Fact]
    public async Task Can_Create_Table()
    {
        await using var c = new NpgsqlConnection(_conn);
        await c.OpenAsync();
        await new NpgsqlCommand("create table if not exists smoke(id int);", c).ExecuteNonQueryAsync();
    }
}
