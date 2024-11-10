using SnackHub.Domain.Base;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Domain.Entities;

public class ProductionOrder : Entity<Guid>, IAggregateRoot
{
    protected ProductionOrder( ): base(Guid.NewGuid()) { }
    
    public ProductionOrder(Guid orderId, IReadOnlyCollection<ProductionOrderItem> items) 
        : this(Guid.NewGuid(), orderId, items, KitchenOrderStatus.Received)
    {
    }
    
    public ProductionOrder(Guid id, Guid orderId, IReadOnlyCollection<ProductionOrderItem> items, KitchenOrderStatus status)
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
    public virtual KitchenOrderStatus Status { get; private set; }

    public void UpdateStatus()
    {
        var previousStatus = Status;
        
        Status = Status switch
        {
            KitchenOrderStatus.Received => KitchenOrderStatus.Preparing,
            KitchenOrderStatus.Preparing => KitchenOrderStatus.Done,
            KitchenOrderStatus.Done => KitchenOrderStatus.Finished,
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