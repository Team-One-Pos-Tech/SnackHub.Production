using FluentAssertions;
using NUnit.Framework;
using SnackHub.Domain.Entities;
using SnackHub.Domain.ValueObjects;

using KitchenOrderFactory = SnackHub.Domain.Entities.ProductionOrder.Factory;
using KitchenOrderItemFactory = SnackHub.Domain.ValueObjects.ProductionOrderItem.Factory;

namespace SnackHub.Domain.Tests.Entities;

public class KitchenOrderShould
{
    [Test]
    public void BeCreatedSuccessfullyWhenRequirementsAreMet()
    {
        var orderId = Guid.NewGuid();
        
        var kitchenOrder = KitchenOrderFactory.Create(orderId, [
            KitchenOrderItemFactory.Create("X-Bacon", 3)
        ]);
        
        kitchenOrder
            .Should()
            .BeEquivalentTo(new
            {
                OrderId = orderId,
                Status = ProductionOrderStatus.Received,
                Items = new []
                {
                    new
                    {
                        ProductName = "X-Bacon",
                        Quantity = 3
                    }
                }
            });
    }
    
    [Test]
    public void ShouldFailOnInvalidOrderId()
    {
        var act = () => KitchenOrderFactory.Create(Guid.Empty, []);
        
        act.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }
    
    [Test]
    public void ShouldFailOnInvalidItems()
    {
        var act = () => KitchenOrderFactory.Create(Guid.NewGuid(), null!);
        
        act.Should().ThrowExactly<ArgumentNullException>();
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