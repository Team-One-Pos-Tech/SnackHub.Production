using BoDi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using SnackHub.Production.Api;

namespace SnackHub.Production.Behavior.Tests.Hooks;

[Binding]
public sealed class EnvironmentSetupHooks
{

    [BeforeTestRun]
    public static async Task BeforeTestRun(IObjectContainer testThreadContainer)
    {
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
    }


    [AfterTestRun]
    public static async Task AfterTestRun(IObjectContainer testThreadContainer)
    {
    }
}