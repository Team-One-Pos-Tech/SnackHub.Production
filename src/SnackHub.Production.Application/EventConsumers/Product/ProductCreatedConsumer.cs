using MassTransit;
using Microsoft.Extensions.Logging;
using SnackHub.Production.Application.Models.Product;
using SnackHub.Production.Domain.Contracts;
using ProductFactory = SnackHub.Production.Domain.Entities.Product;

namespace SnackHub.Production.Application.EventConsumers.Product;

public class ProductCreatedConsumer : IConsumer<ProductCreated>
{
    private readonly ILogger<ProductCreatedConsumer> _logger;
    private readonly IProductRepository _productRepository;

    public ProductCreatedConsumer(
        ILogger<ProductCreatedConsumer> logger, 
        IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task Consume(ConsumeContext<ProductCreated> context)
    {
        _logger.LogInformation("The product [{productName}] has been created by a external service", context.Message.Name);
        
        var product = ProductFactory
            .Create(
                context.Message.Id,
                context.Message.Name, 
                context.Message.Description);
        
        await _productRepository.AddAsync(product);
    }
}