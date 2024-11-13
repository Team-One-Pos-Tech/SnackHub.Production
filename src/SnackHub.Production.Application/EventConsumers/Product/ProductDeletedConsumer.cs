using MassTransit;
using Microsoft.Extensions.Logging;
using SnackHub.Production.Application.Models.Consumers;
using SnackHub.Production.Domain.Contracts;

namespace SnackHub.Production.Application.EventConsumers.Product;

public class ProductDeletedConsumer : IConsumer<ProductDeleted>
{
    private readonly ILogger<ProductDeletedConsumer> _logger;
    private readonly IProductRepository _productRepository;

    public ProductDeletedConsumer(
        ILogger<ProductDeletedConsumer> logger, 
        IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public async Task Consume(ConsumeContext<ProductDeleted> context)
    {
        _logger.LogInformation("The product with id [{productId}] has been removed by a external service", context.Message.Id);
        await _productRepository.RemoveAsync(context.Message.Id);
    }
}