using System;
using TechTalk.SpecFlow;

namespace SnackHub.Production.Behavior.Tests.StepDefinitions
{
    [Binding]
    public class UpdateProductionOrderStepDefinitions
    {
        [Given(@"existing a production order is in Received status")]
        public void GivenExistingAProductionOrderIsInReceivedStatus()
        {
            throw new PendingStepException();
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
}
