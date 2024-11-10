using SnackHub.OrderService.Api.Configuration;
using SnackHub.Production.Application.EventConsumers.Product;

using MassTransit;

namespace SnackHub.Production.Api.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransit(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var settings = configuration.GetSection("RabbitMQ").Get<RabbitMQSettings>();
        
        serviceCollection.AddMassTransit(busConfigurator =>
        {
            // busConfigurator.AddConsumer<PaymentApprovedConsumer>();
            // busConfigurator.AddConsumer<PaymentRejectedConsumer>();
            //
            // busConfigurator.AddConsumer<ProductionOrderAcceptedConsumer>();
            // busConfigurator.AddConsumer<ProductionOrderCompletedConsumer>();

            busConfigurator.AddConsumer<ProductCreatedConsumer>();
            busConfigurator.AddConsumer<ProductUpdatedConsumer>();
            busConfigurator.AddConsumer<ProductDeletedConsumer>();
            
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                // configurator.ReceiveEndpoint(settings.Host, queueConfigurator =>
                // {
                //     queueConfigurator.AutoDelete = true;
                // });
                
                configurator.Host(settings.Host, "/",  rabbitMqHostConfigurator =>
                {
                    rabbitMqHostConfigurator.Username(settings.UserName);
                    rabbitMqHostConfigurator.Password(settings.Password);							
                });
                
                configurator.AutoDelete = true;
                configurator.ConfigureEndpoints(context);
            });
            
        });
        
        return serviceCollection;
    }
}