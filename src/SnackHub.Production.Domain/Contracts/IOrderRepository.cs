using SnackHub.Production.Domain.Entities;

namespace SnackHub.Production.Domain.Contracts;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task EditAsync(Order order);
    Task<Order?> GetByIdAsync(Guid id);
    Task<IEnumerable<Order>> ListAllAsync();
}