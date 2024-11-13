using MassTransit;

namespace SnackHub.Production.Application.Models.Consumers;

[MessageUrn("snack-hub-products")]
[EntityName("product-deleted")]
public record ProductDeleted(Guid Id);