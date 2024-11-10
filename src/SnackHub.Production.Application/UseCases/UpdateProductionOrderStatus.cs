using SnackHub.Domain.Contracts;
using SnackHub.Production.Application.Contracts;
using SnackHub.Production.Application.Models.Requests;
using SnackHub.Production.Application.Models.Responses;

namespace SnackHub.Production.Application.UseCases;

public class UpdateProductionOrderStatus(IProductionOrderRepository productionOrderRepository) : IUpdateKitchenOrderStatusUseCase
{

    public async Task<UpdateKitchenOrderStatusResponse> Execute(Models.Requests.UpdateProductionOrderStatus orderStatusRequest)
    {
        var response = new UpdateKitchenOrderStatusResponse();

        var kitchenOrder = await productionOrderRepository.GetByOderIdAsync(orderStatusRequest.OrderId);
        if (kitchenOrder is null)
        {
            response.AddNotification(nameof(orderStatusRequest.OrderId), "Kitchen request for order not found!");
            return response;
        }

        try
        {
            kitchenOrder.UpdateStatus();
            await productionOrderRepository.EditAsync(kitchenOrder);
        }
        catch (Exception exception)
        {
            response.AddNotification(nameof(orderStatusRequest.OrderId), exception.Message);
        }

        return response;
    }
}