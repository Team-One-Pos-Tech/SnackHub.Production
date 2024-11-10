using SnackHub.Domain.Contracts;
using SnackHub.Production.Application.Contracts;
using SnackHub.Production.Application.Models;

namespace SnackHub.Production.Application.UseCases;

public class ListKitchenOrderUseCase : IListKitchenOrderUseCase
{
    private readonly IKitchenOrderRepository _kitchenOrderRepository;

    public ListKitchenOrderUseCase(IKitchenOrderRepository kitchenOrderRepository)
    {
        _kitchenOrderRepository = kitchenOrderRepository;
    }

    public async Task<IEnumerable<KitchenOrderResponse>> Execute()
    {
        var kitchenRequests = await _kitchenOrderRepository.ListCurrentAsync();

        return kitchenRequests.Select(o => new KitchenOrderResponse
        {
            OrderId = o.OrderId,
            Items = o.Items.Select(i => (i.ProductName, i.Quantity)).ToList(),
            Status = o.Status.ToString(),
            CreatedAt = o.CreatedAt,
            UpdatedAt = o.UpdatedAt
        }).ToList();
    }
}