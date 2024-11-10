namespace SnackHub.Production.Application.Models.Requests;

public class UpdateProductionOrderStatus
{
    public required Guid OrderId { get; init; }
}