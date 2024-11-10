using SnackHub.Domain.Contracts;
using SnackHub.Domain.Entities;
using SnackHub.Domain.ValueObjects;
using SnackHub.Production.Application.Contracts;
using SnackHub.Production.Application.Models;

namespace SnackHub.Production.Application.UseCases;

public class CreateProductionOrder(
    IProductionOrderRepository _productionOrderRepository) : ICreateProductionOrder
{

    public async Task<CreateKitchenOrderResponse> Execute(CreateProductionOrderRequest request)
    {
        var response = new CreateKitchenOrderResponse();

        List<ProductionOrderItem> items = MapProductionOrderItems(request);

        var productionOrder = ProductionOrder.Factory.Create(request.OrderId, items);

        await _productionOrderRepository.AddAsync(productionOrder);

        return response;
    }

    private static List<ProductionOrderItem> MapProductionOrderItems(CreateProductionOrderRequest request)
    {
        return request
            .Items
            .Select(orderItem => ProductionOrderItem.Factory.Create(orderItem.ProductName, orderItem.Quantity))
            .ToList();
    }
}