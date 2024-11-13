using MassTransit;

namespace SnackHub.Production.Application.Models.Consumers;

[MessageUrn("snack-hub-products")]
[EntityName("product-created")]
public record ProductCreated(Guid Id, string Name, decimal Price, string Description);