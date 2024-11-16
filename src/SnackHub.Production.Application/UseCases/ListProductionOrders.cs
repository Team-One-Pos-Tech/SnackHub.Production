using SnackHub.Production.Application.Contracts;
using SnackHub.Production.Application.Models.Responses;
using SnackHub.Production.Domain.Contracts;
using SnackHub.Production.Domain.ValueObjects;

namespace SnackHub.Production.Application.UseCases;

public class ListProductionOrders(IProductionOrderRepository productionOrderRepository) : IListProductionOrders
{
    public async Task<IEnumerable<ProductionOrderResponse>> Get()
    {
        var productionOrders = await productionOrderRepository.ListAllAsync();

        return productionOrders.Select(o => new ProductionOrderResponse
        {
            Id = o.Id,
            OrderId = o.OrderId,
            Items = o.Items.Select(i => new ProductionItemResponse { 
                ProductId = i.ProductId, Quantity = i.Quantity
            }).ToList(),
            Status = o.Status.ToString(),
            CreatedAt = o.CreatedAt,
            UpdatedAt = o.UpdatedAt
        }).ToList();
    }
}