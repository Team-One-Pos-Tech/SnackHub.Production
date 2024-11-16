using SnackHub.Production.Domain.Base;
using SnackHub.Production.Domain.ValueObjects;

namespace SnackHub.Production.Domain.Entities;

public class ProductionOrder : Entity<Guid>, IAggregateRoot
{
    public ProductionOrder(Guid orderId, List<ProductionOrderItem> items)
        : this(Guid.NewGuid(), orderId, ProductionOrderStatus.Received)
    {
        Items = items ?? throw new ArgumentNullException(nameof(items));
    }

    public ProductionOrder(Guid id, Guid orderId, ProductionOrderStatus status)
        : base(id)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(orderId, Guid.Empty);

        OrderId = orderId;
        Status = status;
        Items = new List<ProductionOrderItem>();
    }

    public virtual Guid OrderId { get; private set; }
    public virtual List<ProductionOrderItem> Items { get; private set; }
    public virtual ProductionOrderStatus Status { get; private set; }

    public void UpdateStatus()
    {
        var previousStatus = Status;

        Status = Status switch
        {
            ProductionOrderStatus.Received => ProductionOrderStatus.Preparing,
            ProductionOrderStatus.Preparing => ProductionOrderStatus.Done,
            ProductionOrderStatus.Done => ProductionOrderStatus.Finished,
            _ => Status
        };

        if (previousStatus != Status)
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public static class Factory
    {
        public static ProductionOrder Create(Guid orderId, List<ProductionOrderItem> items)
        {
            return new ProductionOrder(orderId, items);
        }
    }
}
