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

        //var items = order
        //    .Items
        //    .Select(orderItem => ProductionOrderItem.Factory.Create(orderItem.ProductName, orderItem.Quantity))
        //    .ToList();

        await _productionOrderRepository.AddAsync(new Domain.Entities.ProductionOrder(request.OrderId, []));

        return response;

    }
}