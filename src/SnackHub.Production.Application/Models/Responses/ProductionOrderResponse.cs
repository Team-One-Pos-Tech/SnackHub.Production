using Flunt.Notifications;

namespace SnackHub.Production.Application.Models.Responses;

public class ProductionOrderResponse : Notifiable<Notification>
{
    public required Guid Id { get; init; }
    public required Guid OrderId { get; init; }
    public required IEnumerable<ProductionItemResponse> Items { get; init; } = [];
    public required string Status { get; init; } = string.Empty;
    public required DateTime CreatedAt { get; init; }
    public required DateTime? UpdatedAt { get; init; }
}