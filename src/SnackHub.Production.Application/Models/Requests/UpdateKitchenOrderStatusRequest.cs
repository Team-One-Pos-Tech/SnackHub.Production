namespace SnackHub.Production.Application.Models.Requests;

public class UpdateKitchenOrderStatusRequest
{
    public required Guid OrderId { get; init; }
}