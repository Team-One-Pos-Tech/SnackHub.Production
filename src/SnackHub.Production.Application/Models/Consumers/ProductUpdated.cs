using MassTransit;

namespace SnackHub.Production.Application.Models.Consumers;

[MessageUrn("snack-hub-products")]
[EntityName("product-updated")]
public record ProductUpdated(Guid Id, string Name, decimal Price, string Description);