using DotNet.Testcontainers.Networks;
using Npgsql;
using Testcontainers.PostgreSql;

namespace SnackHub.Production.Behavior.Tests.Containers;

internal class Postgresql
{
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
            .WithDatabase("SnackHub-ProductionService")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .WithPortBinding(5432)
            .WithNetwork(_network)
            .WithNetworkAliases("postgres")
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
