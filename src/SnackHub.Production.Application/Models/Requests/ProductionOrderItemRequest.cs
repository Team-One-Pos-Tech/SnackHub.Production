namespace SnackHub.Production.Application.Models.Requests;

public class ProductionOrderItemRequest
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
}