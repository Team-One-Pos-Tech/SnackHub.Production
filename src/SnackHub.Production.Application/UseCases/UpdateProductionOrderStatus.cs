using SnackHub.Domain.Contracts;
using SnackHub.Production.Application.Contracts;
using SnackHub.Production.Application.Models.Requests;
using SnackHub.Production.Application.Models.Responses;

namespace SnackHub.Production.Application.UseCases;

public class UpdateProductionOrderStatus : IUpdateKitchenOrderStatusUseCase
{
    private readonly IProductionOrderRepository _kitchenOrderRepository;

    public UpdateProductionOrderStatus(IProductionOrderRepository kitchenOrderRepository)
    {
        _kitchenOrderRepository = kitchenOrderRepository;
    }

    public async Task<UpdateKitchenOrderStatusResponse> Execute(Models.Requests.UpdateProductionOrderStatus orderStatusRequest)
    {
        var response = new UpdateKitchenOrderStatusResponse();

        var kitchenOrder = await _kitchenOrderRepository.GetByOderIdAsync(orderStatusRequest.OrderId);
        if (kitchenOrder is null)
        {
            response.AddNotification(nameof(orderStatusRequest.OrderId), "Kitchen request for order not found!");
            return response;
        }

        try
        {
            kitchenOrder.UpdateStatus();
            await _kitchenOrderRepository.EditAsync(kitchenOrder);
        }
        catch (Exception exception)
        {
            response.AddNotification(nameof(orderStatusRequest.OrderId), exception.Message);
        }

        return response;
    }
}