namespace SnackHub.Production.Behavior.Tests.StepDefinitions;

[Binding]
public class UpdateProductionOrderStepDefinitions(
    ProductionOrderApiClient productionApiClient,
    ScenarioContext scenarioContext
    )
{
    [Given(@"existing a production order is in Received status")]
    public async Task GivenExistingAProductionOrderIsInReceivedStatusAsync()
    {
        var createProductionOrderRequest = new CreateProductionOrderRequest()
        {
            OrderId = Guid.NewGuid(),
            Items =
            [
                new()
                {
                   Id = Guid.NewGuid(),
                   Quantity = 10
                }
            ]
        };

        var response = await productionApiClient.CreateProductionOrderAsync(createProductionOrderRequest);

        scenarioContext["createProductionOrderRequest"] = createProductionOrderRequest;
    }

    [When(@"Update Status")]
    public async Task WhenUpdateStatusAsync()
    {
        var createProductionOrderRequest = scenarioContext.Get<CreateProductionOrderRequest>("createProductionOrderRequest");

        var request = new UpdateStatusRequest()
        {
            OrderId = createProductionOrderRequest.OrderId
        };

        var response = await productionApiClient.UpdateStatusAsync(request);
    }

    [Then(@"the Production Order should be in in Preparing status")]
    public async Task ThenTheProductionOrderShouldBeInInPreparingStatusAsync()
    {
        var createProductionOrderRequest = scenarioContext.Get<CreateProductionOrderRequest>("createProductionOrderRequest");

        var allOrders = await productionApiClient.GetAllProductionOrdersAsync();

        var updatedOrder = allOrders.First(x => x.OrderId == createProductionOrderRequest.OrderId);

        updatedOrder.Status.Should().Be("Preparing");
    }
}