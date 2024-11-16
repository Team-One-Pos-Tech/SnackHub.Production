using SnackHub.Production.Domain.Base;

namespace SnackHub.Production.Domain.Entities;

public class ProductionOrderItem
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }

    public ProductionOrderItem(Guid productId, int quantity)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
    }
}