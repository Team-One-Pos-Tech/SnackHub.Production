using MassTransit;
using SnackHub.Production.Application.Contracts;
using SnackHub.Production.Application.Models.Consumers;
using SnackHub.Production.Application.Models.Requests;
using SnackHub.Production.Application.Models.Responses;
using SnackHub.Production.Domain.Contracts;
using SnackHub.Production.Domain.Entities;
using SnackHub.Production.Domain.ValueObjects;

namespace SnackHub.Production.Application.UseCases;

public class CreateProductionOrder(
    IProductionOrderRepository _productionOrderRepository) : ICreateProductionOrder
{

    public async Task<CreateProductionOrderResponse> Execute(CreateProductionOrderRequest request)
    {
        var response = new CreateProductionOrderResponse();

        List<ProductionOrderItem> items = MapProductionOrderItems(request);

        var productionOrder = ProductionOrder.Factory.Create(request.OrderId, items);

        await _productionOrderRepository.AddAsync(productionOrder);

        return response;
    }

    private static List<ProductionOrderItem> MapProductionOrderItems(CreateProductionOrderRequest request)
    {
        return request
            .Items
            .Select(orderItem => new ProductionOrderItem(orderItem.ProductId, orderItem.Quantity))
            .ToList();
    }
}