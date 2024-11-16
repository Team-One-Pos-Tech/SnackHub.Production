namespace SnackHub.Production.Application.Models.Requests;

public record CreateProductionOrderRequest
{
    public CreateProductionOrderRequest(Guid orderId)
    {
        OrderId = orderId;
        Items = [];
    }

    public Guid OrderId { get; init; }

    public IEnumerable<ProductionItemRequest> Items { get; set; }
}