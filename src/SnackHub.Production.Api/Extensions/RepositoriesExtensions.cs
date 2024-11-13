using Microsoft.EntityFrameworkCore;
using SnackHub.Production.Api.Configuration;
using SnackHub.Production.Domain.Contracts;
using SnackHub.Production.Infra.Repositories;
using SnackHub.Production.Infra.Repositories.Context;

namespace SnackHub.Production.Api.Extensions;

public static class RepositoriesExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        // serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

        serviceCollection
            .AddScoped<IProductionOrderRepository, ProductionOrderRepository>()
            .AddScoped<IProductRepository, ProductRepository>();
        
        return serviceCollection;
    }

    public static IServiceCollection AddDatabaseContext(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var settings = configuration.GetSection("Storage:PostgreSQL").Get<PostgreSQLSettings>();
        var connectionString = $"Host={settings.Host};Username={settings.UserName};Password={settings.Password};Database={settings.Database}";
        
        serviceCollection
            .AddDbContext<ProductionDbContext>(options => 
                options.UseNpgsql(connectionString));

        return serviceCollection;
    }
}