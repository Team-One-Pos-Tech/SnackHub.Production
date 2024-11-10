using SnackHub.Production.Domain.Entities;

namespace SnackHub.Production.Domain.Contracts;

public interface IProductionOrderRepository
{
    Task AddAsync(ProductionOrder productionOrder);
    Task EditAsync(ProductionOrder productionOrder);
    Task<ProductionOrder?> GetByOderIdAsync(Guid orderId);
    Task<IEnumerable<ProductionOrder>> ListAllAsync();
    Task<IEnumerable<ProductionOrder>> ListCurrentAsync();
}