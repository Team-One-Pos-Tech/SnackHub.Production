using MassTransit;

namespace SnackHub.Production.Application.Models.Consumers;

[MessageUrn("snack-hub-production")]
[EntityName("production-order-submitted")]
public record ProductionOrderSubmittedRequest(Guid OrderId, IEnumerable<ProductionOrderProductDetails> ProductList);

public record ProductionOrderProductDetails(Guid ProductId, int Quantity);