using Flunt.Notifications;

namespace SnackHub.Production.Application.Models;

public class KitchenOrderResponse : Notifiable<Notification>
{
    public required Guid OrderId { get; init; }
    public required IEnumerable<(string ProductName, int Quantity)> Items { get; init; } = [];
    public required string Status { get; init; } = string.Empty;
    public required DateTime CreatedAt { get; init; }
    public required DateTime? UpdatedAt { get; init; }
}