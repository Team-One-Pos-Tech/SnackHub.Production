using SnackHub.Domain.Contracts;
using SnackHub.Production.Application.Contracts;
using SnackHub.Production.Application.Models.Responses;

namespace SnackHub.Production.Application.UseCases;

public class ListProductionOrders(IProductionOrderRepository productionOrderRepository) : IListProductionOrders
{
    public async Task<IEnumerable<ProductionOrderResponse>> Get()
    {
        var kitchenRequests = await productionOrderRepository.ListCurrentAsync();

        return kitchenRequests.Select(o => new ProductionOrderResponse
        {
            OrderId = o.OrderId,
            Items = o.Items.Select(i => (i.ProductName, i.Quantity)).ToList(),
            Status = o.Status.ToString(),
            CreatedAt = o.CreatedAt,
            UpdatedAt = o.UpdatedAt
        }).ToList();
    }
}