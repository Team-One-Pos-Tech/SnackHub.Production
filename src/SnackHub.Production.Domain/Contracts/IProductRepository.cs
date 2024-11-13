using SnackHub.Production.Domain.Entities;

namespace SnackHub.Production.Domain.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task AddAsync(Product product);
        Task EditAsync(Product product);
        Task RemoveAsync(Guid id);
        Task<Product?> GetProductByIdAsync(Guid id);
    }
}
