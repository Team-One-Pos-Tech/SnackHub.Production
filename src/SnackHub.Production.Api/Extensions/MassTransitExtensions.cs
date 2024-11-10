using SnackHub.Production.Application.EventConsumers.Product;

using MassTransit;
using SnackHub.Production.Api.Configuration;

namespace SnackHub.Production.Api.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransit(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var settings = configuration.GetSection("RabbitMQ").Get<RabbitMQSettings>();
        
        serviceCollection.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumer<ProductCreatedConsumer>();
            busConfigurator.AddConsumer<ProductUpdatedConsumer>();
            busConfigurator.AddConsumer<ProductDeletedConsumer>();
            
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                // Shared messages - those will be in a replicated queue at RabbitMQ
                configurator.ReceiveEndpoint("production-service", rabbitMqReceiveEndpointConfigurator =>
                {
                    rabbitMqReceiveEndpointConfigurator.ConfigureConsumer<ProductCreatedConsumer>(context);
                    rabbitMqReceiveEndpointConfigurator.ConfigureConsumer<ProductUpdatedConsumer>(context);
                    rabbitMqReceiveEndpointConfigurator.ConfigureConsumer<ProductDeletedConsumer>(context);
                });
                
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