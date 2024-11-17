using MassTransit;
using Microsoft.Extensions.Logging;
using SnackHub.Production.Application.Contracts;
using SnackHub.Production.Application.EventConsumers.Product;
using SnackHub.Production.Application.Models.Consumers;
using SnackHub.Production.Application.Models.Requests;
using SnackHub.Production.Application.Models.Responses;
using SnackHub.Production.Domain.Contracts;
using SnackHub.Production.Domain.Entities;

namespace SnackHub.Production.Application.UseCases;

public class CreateProductionOrder(
    ILogger<CreateProductionOrder> logger,
    IProductionOrderRepository _productionOrderRepository) : ICreateProductionOrder
{

    public async Task<CreateProductionOrderResponse> Execute(CreateProductionOrderRequest request)
    {
        logger.LogInformation("Creating production order [{orderId}]", request.OrderId);

        List<ProductionOrderItem> items = MapProductionOrderItems(request);

        var productionOrder = ProductionOrder.Factory.Create(request.OrderId, items);

        await _productionOrderRepository.AddAsync(productionOrder);

        logger.LogInformation("Production order [{productionOrderId}] created", productionOrder.Id);

        return new CreateProductionOrderResponse(productionOrder.Id);
    }

    private static List<ProductionOrderItem> MapProductionOrderItems(CreateProductionOrderRequest request)
    {
        return request
            .Items
            .Select(orderItem => new ProductionOrderItem(orderItem.ProductId, orderItem.Quantity))
            .ToList();
    }
}