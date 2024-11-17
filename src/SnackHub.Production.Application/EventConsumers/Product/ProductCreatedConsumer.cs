using MassTransit;
using Microsoft.Extensions.Logging;
using SnackHub.Production.Application.Models.Consumers;
using SnackHub.Production.Domain.Contracts;
using ProductFactory = SnackHub.Production.Domain.Entities.Product;

namespace SnackHub.Production.Application.EventConsumers.Product;

public class ProductCreatedConsumer(
    ILogger<ProductCreatedConsumer> logger,
    IProductRepository productRepository) : IConsumer<ProductCreated>
{
    public async Task Consume(ConsumeContext<ProductCreated> context)
    {
        logger.LogInformation("The product [{productName}] has been created by a external service", context.Message.Name);
        
        var product = ProductFactory
            .Create(
                context.Message.Id,
                context.Message.Name, 
                context.Message.Description);
        
        await productRepository.AddAsync(product);
    }
}