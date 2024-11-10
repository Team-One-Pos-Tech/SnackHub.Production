using SnackHub.Domain.Contracts;
using SnackHub.Domain.ValueObjects;
using SnackHub.Production.Application.Contracts;
using SnackHub.Production.Application.Models;

namespace SnackHub.Production.Application.UseCases;

public class CreateKitchenOrderUseCase : ICreateKitchenOrderUseCase
{
    private readonly IKitchenOrderRepository _kitchenOrderRepository;
    private readonly IOrderRepository _orderRepository;

    public CreateKitchenOrderUseCase(IOrderRepository orderRepository,
        IKitchenOrderRepository kitchenOrderRepository)
    {
        _orderRepository = orderRepository;
        _kitchenOrderRepository = kitchenOrderRepository;
    }

    public async Task<CreateKitchenOrderResponse> Execute(CreateKitchenOrderRequest request)
    {
        var response = new CreateKitchenOrderResponse();
        var order = await _orderRepository.GetByIdAsync(request.OrderId);

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
                .Select(orderItem => KitchenOrderItem.Factory.Create(orderItem.ProductName, orderItem.Quantity))
                .ToList();

            await _kitchenOrderRepository.AddAsync(new Domain.Entities.KitchenOrder(order.Id, items));

            return response;
        }
        catch (Exception exception)
        {
            response.AddNotification(nameof(request.OrderId), exception.Message);
            return response;
        }
    }
}