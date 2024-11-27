using MassTransit;
using Microsoft.Extensions.Logging;
using SnackHub.Production.Application.Contracts;
using SnackHub.Production.Application.Models.Consumers;
using SnackHub.Production.Application.Models.Requests;
using SnackHub.Production.Application.Models.Responses;
using SnackHub.Production.Domain.Contracts;
using SnackHub.Production.Domain.Entities;

namespace SnackHub.Production.Application.UseCases;

public class UpdateProductionOrderStatus(
    ILogger<UpdateProductionOrderStatus> logger,
    IProductionOrderRepository productionOrderRepository,
    IPublishEndpoint publishEndpoint
    ) : IUpdateProductionOrderStatus
{

    public async Task<UpdateProductionOrderStatusResponse> Execute(UpdateStatusRequest orderStatusRequestRequest)
    {
        logger.LogInformation("Updating production order status [{productionOrderId}]", orderStatusRequestRequest.Id);

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

        logger.LogInformation("Production order [{productionOrderId}] status updated", productionOrder.Id);

        await publishEndpoint.Publish(
            new ProductionOrderStatusUpdated(productionOrder!.OrderId, productionOrder!.Status));

        return response;
    }

    public static bool IsRequestValid(
        ProductionOrder? productionOrder,
        UpdateStatusRequest orderStatusRequestRequest,
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