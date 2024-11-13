using SnackHub.Domain.Base;
using SnackHub.Domain.ValueObjects;
using SnackHub.Production.Domain.ValueObjects;

namespace SnackHub.Production.Domain.Entities;

public class ProductionOrder : Entity<Guid>, IAggregateRoot
{
    
    public ProductionOrder(Guid orderId, IReadOnlyCollection<ProductionOrderItem> items) 
        : this(Guid.NewGuid(), orderId, items, ProductionOrderStatus.Received)
    {
    }
    
    public ProductionOrder(Guid id, Guid orderId, IReadOnlyCollection<ProductionOrderItem> items, ProductionOrderStatus status)
        : base(id)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(orderId, Guid.Empty);
        ArgumentNullException.ThrowIfNull(items);
        
        OrderId = orderId;
        Items = items;
        Status = status;
    }
    
    public virtual Guid OrderId { get; private set; }
    public virtual IReadOnlyCollection<ProductionOrderItem> Items { get; private set; }
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
        public static ProductionOrder Create(Guid orderId, IReadOnlyCollection<ProductionOrderItem> items)
        {
            return new ProductionOrder(orderId, items);
        }
    }
}