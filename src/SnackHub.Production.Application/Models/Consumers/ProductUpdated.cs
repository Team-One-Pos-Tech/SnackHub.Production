using MassTransit;

namespace SnackHub.Production.Application.Models.Product;

[MessageUrn("snack-hub-products")]
[EntityName("product-updated")]
public record ProductUpdated(Guid Id, string Name, decimal Price, string Description);