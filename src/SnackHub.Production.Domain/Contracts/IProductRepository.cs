using SnackHub.Domain.Entities;

namespace SnackHub.Domain.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task AddAsync(Product product);
        Task EditAsync(Product product);
        Task RemoveAsync(Guid id);
        Task<Product> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Product>> ListAllAsync();
        Task<IEnumerable<Product>> GetByCategory(Category category);
    }
}
