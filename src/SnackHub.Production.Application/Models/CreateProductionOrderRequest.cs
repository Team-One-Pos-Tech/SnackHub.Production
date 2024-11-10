using SnackHub.Application.Tests.UseCases;

namespace SnackHub.Production.Application.Models;

public record CreateProductionOrderRequest
{
    public CreateProductionOrderRequest(Guid orderId)
    {
        OrderId = orderId;
        Items = [];
    }

    public Guid OrderId { get; init; }

    public IEnumerable<ProductionOrderItemRequest> Items { get; set; }
}