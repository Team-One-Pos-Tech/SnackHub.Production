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
        var order = new Order(default, []);

        if (order is null)
        {
            response.AddNotification(nameof(request.OrderId), "Order not found");
            return response;
        }

        if (order.Status is not OrderStatus.Accepted)
        {
            response.AddNotification(nameof(request.OrderId), "Only confirmed orders can be send to the kitchen");
            return response;
        }

        try
        {
            var items = order
                .Items
                .Select(orderItem => ProductionOrderItem.Factory.Create(orderItem.ProductName, orderItem.Quantity))
                .ToList();

            await _productionOrderRepository.AddAsync(new Domain.Entities.ProductionOrder(order.Id, items));

            return response;
        }
        catch (Exception exception)
        {
            response.AddNotification(nameof(request.OrderId), exception.Message);
            return response;
        }
    }
}