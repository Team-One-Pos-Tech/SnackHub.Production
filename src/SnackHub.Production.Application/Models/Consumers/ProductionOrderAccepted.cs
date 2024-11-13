using MassTransit;

namespace SnackHub.Production.Application.Models.Consumers;

[MessageUrn("snack-hub-production")]
[EntityName("production-order-accepted")]
public record ProductionOrderAccepted(Guid OrderId);