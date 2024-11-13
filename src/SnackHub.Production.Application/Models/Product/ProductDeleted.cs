using MassTransit;

namespace SnackHub.Production.Application.Models.Product;

[MessageUrn("snack-hub-products")]
[EntityName("product-deleted")]
public record ProductDeleted(Guid Id);