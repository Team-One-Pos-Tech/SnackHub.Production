using SnackHub.Domain.Base;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Domain.Entities;

public class KitchenOrder : Entity<Guid>, IAggregateRoot
{
    protected KitchenOrder( ): base(Guid.NewGuid()) { }
    
    public KitchenOrder(Guid orderId, IReadOnlyCollection<KitchenOrderItem> items) 
        : this(Guid.NewGuid(), orderId, items, KitchenOrderStatus.Received)
    {
    }
    
    public KitchenOrder(Guid id, Guid orderId, IReadOnlyCollection<KitchenOrderItem> items, KitchenOrderStatus status)
        : base(id)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(orderId, Guid.Empty);
        ArgumentNullException.ThrowIfNull(items);
        
        OrderId = orderId;
        Items = items;
        Status = status;
    }
    
    public virtual Guid OrderId { get; private set; }
    public virtual IReadOnlyCollection<KitchenOrderItem> Items { get; private set; }
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
        public static KitchenOrder Create(Guid orderId, IReadOnlyCollection<KitchenOrderItem> items)
        {
            return new KitchenOrder(orderId, items);
        }
    }
}