using SnackHub.Domain.Entities;

namespace SnackHub.Domain.Contracts;

public interface IKitchenOrderRepository
{
    Task AddAsync(KitchenOrder kitchenOrder);
    Task EditAsync(KitchenOrder kitchenOrder);
    Task<KitchenOrder?> GetByOderIdAsync(Guid orderId);
    Task<IEnumerable<KitchenOrder>> ListAllAsync();
    Task<IEnumerable<KitchenOrder>> ListCurrentAsync();
}