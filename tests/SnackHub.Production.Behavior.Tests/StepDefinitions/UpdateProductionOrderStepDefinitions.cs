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
                   ProductId = Guid.NewGuid(),
                   Quantity = 10
                }
            ]
        };

        var response = await productionApiClient.CreateProductionOrderAsync(createProductionOrderRequest);

        scenarioContext["createProductionOrderRequest"] = createProductionOrderRequest;
        scenarioContext["productionOrderId"] = response.Id;
    }

    [When(@"Update Status")]
    public async Task WhenUpdateStatusAsync()
    {
        var productionOrderId = scenarioContext.Get<Guid>("productionOrderId");

        var request = new UpdateStatusRequest()
        {
            Id = productionOrderId
        };

        var response = await productionApiClient.UpdateStatusAsync(request);
    }

    [Then(@"the Production Order should be in in Preparing status")]
    public async Task ThenTheProductionOrderShouldBeInInPreparingStatusAsync()
    {
        var productionOrderId = scenarioContext.Get<Guid>("productionOrderId");

        var allOrders = await productionApiClient.GetAllProductionOrdersAsync();

        var updatedOrder = allOrders.First(x => x.Id == productionOrderId);

        updatedOrder.Status.Should().Be("Preparing");
    }
}