namespace SnackHub.Production.Application.Models;

public class UpdateKitchenOrderStatusRequest
{
    public required Guid OrderId { get; init; }
}