
namespace SnackHub.Application.Tests.UseCases;

public class ProductionOrderItemRequest
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
}