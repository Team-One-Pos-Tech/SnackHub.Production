using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Networks;
using Npgsql;
using Testcontainers.PostgreSql;

namespace SnackHub.Production.Behavior.Tests.Containers;

internal class Postgresql
{
    
    private const int PostgresPublicPort = 5432;
    private const string PostgresRootPassword = "postgres";
    
    private PostgreSqlContainer? _postgreSqlContainer = null;
    private readonly INetwork _network;

    public Postgresql(INetwork network)
    {
        _network = network;
    }

    public string ConnectionString
        => _postgreSqlContainer!.GetConnectionString();

    public async Task InitializeAsync()
    {
        _postgreSqlContainer = new PostgreSqlBuilder()
            .WithDatabase("SnackHubIntegrationTests")
            .WithUsername("postgres")
            .WithPassword(PostgresRootPassword)
            .WithPortBinding(PostgresPublicPort, true)
            .WithCleanUp(true)
            .WithWaitStrategy(Wait
                .ForUnixContainer()
                .UntilPortIsAvailable(PostgresPublicPort))
            .Build();
        
        await _postgreSqlContainer!.StartAsync();

        var dataSourceBuilder = new NpgsqlDataSourceBuilder
        (
            _postgreSqlContainer.GetConnectionString()
        );

        var dataSource = dataSourceBuilder.EnableDynamicJson().Build();
    }

    public Task DisposeAsync()
    {
        return _postgreSqlContainer!.DisposeAsync().AsTask();
    }
}
