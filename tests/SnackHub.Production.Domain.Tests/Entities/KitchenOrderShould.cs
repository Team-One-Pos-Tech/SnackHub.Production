using FluentAssertions;
using NUnit.Framework;
using SnackHub.Production.Domain.Entities;
using SnackHub.Production.Domain.ValueObjects;

namespace SnackHub.Domain.Tests.Entities;

public class KitchenOrderShould
{
    [Test]
    public void BeCreatedSuccessfullyWhenRequirementsAreMet()
    {
        var orderId = Guid.NewGuid();
        var productId = Guid.NewGuid();

        var kitchenOrder = new ProductionOrder(orderId, [
            new ProductionOrderItem(productId, 3)
        ]);
        
        kitchenOrder
            .Should()
            .BeEquivalentTo(new
            {
                OrderId = orderId,
                Status = ProductionOrderStatus.Received,
                Items = new []
                {
                    new ProductionOrderItem(productId, 3)
                }
            });
    }

    [Test]
    public void ShouldTransitionStatusAsExpected(
        [ValueSource(nameof(StatusTransitions))] (ProductionOrderStatus From, ProductionOrderStatus To) transition)
    {
        var kitchenOrder = new ProductionOrder(Guid.NewGuid(), Guid.NewGuid(), [], transition.From);
        
        kitchenOrder.UpdateStatus();
        
        kitchenOrder.Status.Should().Be(transition.To);
    }
  
    private static readonly (ProductionOrderStatus From, ProductionOrderStatus To)[] StatusTransitions =
    [
        (ProductionOrderStatus.Received, ProductionOrderStatus.Preparing),
        (ProductionOrderStatus.Preparing, ProductionOrderStatus.Done),
        (ProductionOrderStatus.Done, ProductionOrderStatus.Finished),
        (ProductionOrderStatus.Finished, ProductionOrderStatus.Finished)
    ];
}