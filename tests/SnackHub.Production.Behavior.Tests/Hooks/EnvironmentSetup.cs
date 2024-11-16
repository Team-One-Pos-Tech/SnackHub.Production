using BoDi;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Networks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Npgsql;
using SnackHub.Production.Api;
using SnackHub.Production.Behavior.Tests.Containers;

namespace SnackHub.Production.Behavior.Tests.Hooks;

[Binding]
public sealed class EnvironmentSetupHooks
{
    private static INetwork _network;
    private static Postgresql _postgresql;

    [BeforeTestRun]
    public static async Task BeforeTestRun(IObjectContainer testThreadContainer)
    {
        _network = new NetworkBuilder().Build();
        _postgresql = new Postgresql(_network);
        await _postgresql.InitializeAsync();

        HttpClient? apiHttpClient;

        var application = new WebApplicationFactory<Program>()

        .WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Development");
        });

        apiHttpClient = application.CreateClient();

        var productionOrderApiClient =
            new ProductionOrderApiClient("", apiHttpClient);

        testThreadContainer.RegisterInstanceAs(productionOrderApiClient);

        using var connection = new NpgsqlConnection(_postgresql.ConnectionString);

        connection.Open();
    }


    [AfterTestRun]
    public static async Task AfterTestRun(IObjectContainer testThreadContainer)
    {
        await _postgresql.DisposeAsync();
    }

    public static string GetConnectionString()
    {
        return _postgresql.ConnectionString;
    }
}