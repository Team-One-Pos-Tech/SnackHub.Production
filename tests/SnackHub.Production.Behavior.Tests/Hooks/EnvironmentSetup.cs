using BoDi;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Networks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Npgsql;
using Siteware.StateMachine.Behaviour.Tests.Extensions;
using SnackHub.Production.Api;
using SnackHub.Production.Behavior.Tests.Containers;

namespace SnackHub.Production.Behavior.Tests.Hooks;

[Binding]
public sealed class EnvironmentSetupHooks
{
    private static Postgresql postgresql;

    [BeforeTestRun]
    public static async Task BeforeTestRun(IObjectContainer testThreadContainer)
    {
        var network = new NetworkBuilder().Build();
        postgresql = new Postgresql(network);
        await postgresql.InitializeAsync();

        HttpClient? apiHttpClient;

        var application = new WebApplicationFactory<Program>()

        .WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Development");
            builder.UseMocks();
        });

        apiHttpClient = application.CreateClient();

        var productionOrderApiClient =
            new ProductionOrderApiClient("", apiHttpClient);

        testThreadContainer.RegisterInstanceAs(productionOrderApiClient);

        using var connection = new NpgsqlConnection(postgresql.ConnectionString);

        connection.Open();
    }


    [AfterTestRun]
    public static async Task AfterTestRun(IObjectContainer testThreadContainer)
    {
        await postgresql.DisposeAsync();
    }

    public static string GetConnectionString()
    {
        return postgresql.ConnectionString;
    }
}