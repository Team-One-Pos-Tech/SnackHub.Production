using SnackHub.Production.Domain.Base;

namespace SnackHub.Production.Domain.ValueObjects;

public class ProductionOrderItem : ValueObject
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    
    public ProductionOrderItem(Guid productId, int quantity)
    {   
        ProductId = productId;
        Quantity = quantity;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ProductId;
        yield return Quantity;
    }
}