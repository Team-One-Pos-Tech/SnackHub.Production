namespace SnackHub.Domain.ValueObjects;

public class OrderItem : ValueObject
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; }
    public decimal UnitPrice { get; init; }
    public int Quantity { get; init; }
    public decimal Total => UnitPrice * Quantity;
    
    protected OrderItem(Guid productId, string productName, decimal unitPrice, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfEqual(productId, Guid.Empty);
        ArgumentException.ThrowIfNullOrWhiteSpace(productName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(unitPrice);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ProductId;
        yield return ProductName;
        yield return UnitPrice;
        yield return Quantity;
    }
    
    public static class Factory
    {
        public static OrderItem Create(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            return new OrderItem(productId, productName, unitPrice, quantity);
        }
    }
}