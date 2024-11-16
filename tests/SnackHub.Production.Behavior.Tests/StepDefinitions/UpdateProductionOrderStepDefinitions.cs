using System;
using TechTalk.SpecFlow;

namespace SnackHub.Production.Behavior.Tests.StepDefinitions;

[Binding]
public class UpdateProductionOrderStepDefinitions(
    ProductionApiClient productionApiClient,
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
    public void WhenUpdateStatus()
    {
        throw new PendingStepException();
    }

    [Then(@"the Production Order should be in in Preparing status")]
    public void ThenTheProductionOrderShouldBeInInPreparingStatus()
    {
        throw new PendingStepException();
    }
}