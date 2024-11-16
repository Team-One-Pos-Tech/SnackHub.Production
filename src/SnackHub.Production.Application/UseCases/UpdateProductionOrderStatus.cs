using MassTransit;
using SnackHub.Production.Application.Contracts;
using SnackHub.Production.Application.Models.Requests;
using SnackHub.Production.Application.Models.Responses;
using SnackHub.Production.Domain.Contracts;
using SnackHub.Production.Domain.Entities;

namespace SnackHub.Production.Application.UseCases;

public class UpdateProductionOrderStatus(
    IProductionOrderRepository productionOrderRepository,
    IPublishEndpoint publishEndpoint
    ) : IUpdateProductionOrderStatus
{

    public async Task<UpdateProductionOrderStatusResponse> Execute(Models.Requests.UpdateStatusRequest orderStatusRequestRequest)
    {
        var productionOrder = await productionOrderRepository.GetByOderIdAsync(orderStatusRequestRequest.Id);

        if(!IsRequestValid(
            productionOrder, 
            orderStatusRequestRequest, 
            out UpdateProductionOrderStatusResponse response))
        {
            return response;
        }

        productionOrder!.UpdateStatus();

        await productionOrderRepository.EditAsync(productionOrder);

        //await publishEndpoint.Publish(
        //    new ProductionOrderStatusUpdated(productionOrder!.OrderId, productionOrder!.Status));

        return response;
    }

    public static bool IsRequestValid(
        ProductionOrder? productionOrder,
        Models.Requests.UpdateStatusRequest orderStatusRequestRequest,
        out UpdateProductionOrderStatusResponse response)
    {
        response = new UpdateProductionOrderStatusResponse();

        if (productionOrder is null)
        {
            response.AddNotification(nameof(orderStatusRequestRequest.Id), "Production order not found!");
            return false;
        }

        return true;
    }
}