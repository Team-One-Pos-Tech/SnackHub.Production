using SnackHub.Domain.Entities;

namespace SnackHub.Domain.Contracts;

public interface IProductionOrderRepository
{
    Task AddAsync(ProductionOrder kitchenOrder);
    Task EditAsync(ProductionOrder kitchenOrder);
    Task<ProductionOrder?> GetByOderIdAsync(Guid orderId);
    Task<IEnumerable<ProductionOrder>> ListAllAsync();
    Task<IEnumerable<ProductionOrder>> ListCurrentAsync();
}