using SnackHub.Domain.Base;
using SnackHub.Domain.ValueObjects;

namespace SnackHub.Domain.Entities;

public class Order : Entity<Guid>, IAggregateRoot
{
    public virtual Guid ClientId { get; private set; }
    public virtual IReadOnlyCollection<OrderItem> Items { get; private set; }
    public virtual OrderStatus Status { get; private set; }
    public decimal Total => Items.Sum(o => o.Total);

    protected Order(): base(Guid.NewGuid()) 
    {
        Items = [];
    }
    
    public Order(Guid clientId, IReadOnlyCollection<OrderItem> items) 
        : this(Guid.NewGuid(), clientId, items, OrderStatus.Pending)
    {
    }
    
    public Order(Guid id, Guid clientId, IReadOnlyCollection<OrderItem> items, OrderStatus status)
        : base(id)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(clientId, Guid.Empty);
        ArgumentNullException.ThrowIfNull(items);
        
        ClientId = clientId;
        Items = items;
        Status = status;
    }

    public void Confirm()
    {
        if (Status != OrderStatus.Pending)
        {
            throw new DomainException($"Order is {GetStatusDescription()} and cannot be confirmed at this time");
        }

        if (Items.Count == 0)
        {
            throw new DomainException("Order must have at least one item to be confirmed");
        }

        Status = OrderStatus.Confirmed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (new[] { OrderStatus.Cancelled, OrderStatus.Accepted, OrderStatus.Declined }.Contains(Status))
        {
            throw new DomainException($"Order is already {GetStatusDescription()} and cannot be cancelled at this time");
        }

        Status = OrderStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateOrderStatus(OrderStatus newStatus)
    {
        if (Status == newStatus)
        {
            throw new DomainException($"Order is already {GetStatusDescription()}");
        }

        Status = newStatus;
    }

    public void Checkout(bool accepted)
    {
        if (Status != OrderStatus.Confirmed)
        {
            throw new DomainException($"Order is {GetStatusDescription()} and cannot be checkout out at this time");
        }

        Status = accepted ? OrderStatus.Accepted : OrderStatus.Declined;
        UpdatedAt = DateTime.UtcNow;
    }

    private string? GetStatusDescription()
    {
        return Enum.GetName(Status)?.ToLowerInvariant();
    }

    public static class Factory
    {
        public static Order Create(Guid clientId, IReadOnlyCollection<OrderItem> items)
        {
            return new Order(clientId, items);
        }
    }
}
