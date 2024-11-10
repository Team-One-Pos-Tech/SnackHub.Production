namespace SnackHub.Production.Application.Models;

public record CreateProductionOrderRequest
{
    public CreateProductionOrderRequest(Guid orderId)
    {
        OrderId = orderId;
    }

    public Guid OrderId { get; init; }

}