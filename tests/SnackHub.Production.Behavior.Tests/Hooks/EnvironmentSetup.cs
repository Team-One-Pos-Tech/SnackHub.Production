using BoDi;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Networks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Siteware.StateMachine.Behaviour.Tests.Extensions;
using SnackHub.Production.Api;
using SnackHub.Production.Behavior.Tests.Containers;
using SnackHub.Production.Infra.Repositories.Context;

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

        
        using var connection = new NpgsqlConnection(postgresql.ConnectionString);

        connection.Open();
        
        var application = new WebApplicationFactory<Program>()
            
        .WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Development");
            builder.UseMocks();

            builder.ConfigureServices(collection =>
            {
                collection.RemoveAll<ProductionDbContext>();
                
                
                var dbContextOptions = new DbContextOptionsBuilder<ProductionDbContext>()
                    .UseNpgsql(postgresql.ConnectionString)
                    .Options;

                var productionDbContext  = new ProductionDbContext(dbContextOptions);
                collection.AddSingleton<ProductionDbContext>(_ => productionDbContext);
                
                // collection.AddDbContext<ProductionDbContext>(optionsBuilder 
                //     => optionsBuilder.UseNpgsql(postgresql.ConnectionString));
            });
        });

        apiHttpClient = application.CreateClient();

        var productionOrderApiClient =
            new ProductionOrderApiClient("", apiHttpClient);

        testThreadContainer.RegisterInstanceAs(productionOrderApiClient);
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