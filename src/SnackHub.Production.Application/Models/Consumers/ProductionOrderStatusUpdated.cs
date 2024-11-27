using MassTransit;
using SnackHub.Production.Domain.ValueObjects;


namespace SnackHub.Production.Application.Models.Consumers;

[MessageUrn("snack-hub-production")]
[EntityName("production-order-status-updated")]
public record ProductionOrderStatusUpdated(Guid OrderId, ProductionOrderStatus Status);
