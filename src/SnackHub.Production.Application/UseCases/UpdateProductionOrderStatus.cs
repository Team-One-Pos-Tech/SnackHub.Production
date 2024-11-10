using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;
using SnackHub.Production.Application.Contracts;
using SnackHub.Production.Application.Models.Requests;
using SnackHub.Production.Application.Models.Responses;

namespace SnackHub.Production.Application.UseCases;

public class UpdateProductionOrderStatus(IProductionOrderRepository productionOrderRepository) : IUpdateKitchenOrderStatusUseCase
{

    public async Task<UpdateKitchenOrderStatusResponse> Execute(Models.Requests.UpdateProductionOrderStatus orderStatusRequest)
    {
        var productionOrder = await productionOrderRepository.GetByOderIdAsync(orderStatusRequest.OrderId);

        if(!IsRequestValid(
            productionOrder, 
            orderStatusRequest, 
            out UpdateKitchenOrderStatusResponse response))
        {
            return response;
        }

        productionOrder!.UpdateStatus();

        await productionOrderRepository.EditAsync(productionOrder);

        return response;
    }

    public static bool IsRequestValid(
        ProductionOrder? productionOrder,
        Models.Requests.UpdateProductionOrderStatus orderStatusRequest,
        out UpdateKitchenOrderStatusResponse response)
    {
        response = new UpdateKitchenOrderStatusResponse();

        if (productionOrder is null)
        {
            response.AddNotification(nameof(orderStatusRequest.OrderId), "Production order not found!");
            return false;
        }

        return true;
    }
}