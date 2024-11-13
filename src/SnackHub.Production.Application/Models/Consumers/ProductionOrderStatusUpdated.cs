using MassTransit;
using SnackHub.Production.Domain.ValueObjects;

[MessageUrn("snack-hub-production")]
[EntityName("production-order-status-updated")]
public record ProductionOrderStatusUpdated(Guid OrderId, ProductionOrderStatus Status);
