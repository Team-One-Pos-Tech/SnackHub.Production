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
            busConfigurator.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("production-service"));
            busConfigurator.AddConsumer<ProductCreatedConsumer>();
            busConfigurator.AddConsumer<ProductUpdatedConsumer>();
            busConfigurator.AddConsumer<ProductDeletedConsumer>();
            
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
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