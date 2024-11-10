using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SnackHub.Production.Domain.Contracts;
using SnackHub.Production.Domain.Entities;
using SnackHub.Production.Infra.Repositories.Context;

namespace SnackHub.Production.Infra.Repositories;

public class ProductRepository : BaseRepository<Product, ProductionDbContext>, IProductRepository
{
    public ProductRepository(
        ProductionDbContext dbContext, 
        ILoggerFactory loggerFactory) : base(dbContext, loggerFactory)
    {
    }
    
    public async Task AddAsync(Product? product)
    {
        await InsertAsync(product);
    }

    public async Task EditAsync(Product product)
    {
        await UpdateAsync(product);
    }

    public async Task RemoveAsync(Guid id)
    {
        await DeleteByPredicateAsync(product => product.Id == id);
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await FindByPredicateAsync(product => product.Id.Equals(id));
    }

    public async Task<IEnumerable<Product?>> ListAllAsync()
    {
        return await ListByPredicateAsync(px => true);
    }

    public async Task<IEnumerable<Product?>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        return await ListByPredicateAsync(p => ids.Contains(p.Id));
    }
}