using FluentAssertions;
using NUnit.Framework;
using SnackHub.Domain.Entities;
using SnackHub.Domain.ValueObjects;

using KitchenOrderFactory = SnackHub.Domain.Entities.KitchenOrder.Factory;
using KitchenOrderItemFactory = SnackHub.Domain.ValueObjects.KitchenOrderItem.Factory;

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
                Status = KitchenOrderStatus.Received,
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
        [ValueSource(nameof(StatusTransitions))] (KitchenOrderStatus From, KitchenOrderStatus To) transition)
    {
        var kitchenOrder = new KitchenOrder(Guid.NewGuid(), Guid.NewGuid(), [], transition.From);
        
        kitchenOrder.UpdateStatus();
        
        kitchenOrder.Status.Should().Be(transition.To);
    }
  
    private static readonly (KitchenOrderStatus From, KitchenOrderStatus To)[] StatusTransitions =
    [
        (KitchenOrderStatus.Received, KitchenOrderStatus.Preparing),
        (KitchenOrderStatus.Preparing, KitchenOrderStatus.Done),
        (KitchenOrderStatus.Done, KitchenOrderStatus.Finished),
        (KitchenOrderStatus.Finished, KitchenOrderStatus.Finished)
    ];
}