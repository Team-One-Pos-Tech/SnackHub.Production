namespace SnackHub.Production.Application.Models.Requests;

public class ProductionItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}