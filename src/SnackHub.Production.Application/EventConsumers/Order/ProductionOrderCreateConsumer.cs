using MassTransit;
using SnackHub.Production.Application.Contracts;
using SnackHub.Production.Application.Models.Consumers;
using SnackHub.Production.Application.Models.Requests;
using SnackHub.Production.Domain.ValueObjects;

namespace SnackHub.Production.Application.EventConsumers.Order;

public class ProductionOrderCreateConsumer(ICreateProductionOrder createProductionOrder) : IConsumer<ProductionOrderSubmittedRequest>
{
    public async Task Consume(ConsumeContext<ProductionOrderSubmittedRequest> context)
    {
        var request = new CreateProductionOrderRequest(context.Message.OrderId)
        {
            Items = context.Message.ProductList.Select(x => new ProductionOrderItemRequest
            {
                Id = x.ProductId,
                Quantity = x.Quantity
            })
        };

        await createProductionOrder.Execute(request);

        await context.Publish(new ProductionOrderStatusUpdated(context.Message.OrderId, ProductionOrderStatus.Received));
    }
}
