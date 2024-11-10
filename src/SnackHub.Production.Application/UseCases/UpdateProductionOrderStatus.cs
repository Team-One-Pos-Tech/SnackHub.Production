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

        var productionOrder = await productionOrderRepository.GetByOderIdAsync(orderStatusRequest.OrderId);

        if (productionOrder is null)
        {
            response.AddNotification(nameof(orderStatusRequest.OrderId), "Production order not found!");
            return response;
        }

        productionOrder.UpdateStatus();

        await productionOrderRepository.EditAsync(productionOrder);

        return response;
    }
}