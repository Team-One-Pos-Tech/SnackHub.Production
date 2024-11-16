namespace SnackHub.Production.Application.Models.Requests;

public class UpdateStatusRequest
{
    public required Guid OrderId { get; init; }
}