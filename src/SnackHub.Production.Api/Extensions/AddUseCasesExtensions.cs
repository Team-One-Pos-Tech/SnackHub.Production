using SnackHub.Production.Application.Contracts;
using SnackHub.Production.Application.UseCases;

namespace SnackHub.Production.Api.Extensions;

public static class AddUseCasesExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<ICreateProductionOrder, CreateProductionOrder>()
            .AddScoped<IListProductionOrders, ListProductionOrders>()
            .AddScoped<IUpdateProductionOrderStatus, UpdateProductionOrderStatus>();

        return serviceCollection;
    }
}