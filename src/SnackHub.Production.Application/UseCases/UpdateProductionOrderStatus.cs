using SnackHub.Production.Application.Contracts;
using SnackHub.Production.Application.Models.Requests;
using SnackHub.Production.Application.Models.Responses;
using SnackHub.Production.Domain.Contracts;
using SnackHub.Production.Domain.Entities;

namespace SnackHub.Production.Application.UseCases;

public class UpdateProductionOrderStatus(IProductionOrderRepository productionOrderRepository) : IUpdateProductionOrderStatus
{

    public async Task<UpdateProductionOrderStatusResponse> Execute(Models.Requests.UpdateProductionOrderStatus orderStatusRequest)
    {
        var productionOrder = await productionOrderRepository.GetByOderIdAsync(orderStatusRequest.OrderId);

        if(!IsRequestValid(
            productionOrder, 
            orderStatusRequest, 
            out UpdateProductionOrderStatusResponse response))
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
        out UpdateProductionOrderStatusResponse response)
    {
        response = new UpdateProductionOrderStatusResponse();

        if (productionOrder is null)
        {
            response.AddNotification(nameof(orderStatusRequest.OrderId), "Production order not found!");
            return false;
        }

        return true;
    }
}