using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SnackHub.Production.Domain.Contracts;
using SnackHub.Production.Domain.Entities;
using SnackHub.Production.Infra.Repositories.Context;

namespace SnackHub.Production.Infra.Repositories;

public class ProductionOrderRepository : BaseRepository<ProductionOrder, ProductionDbContext>, IProductionOrderRepository
{
    public ProductionOrderRepository(
        ProductionDbContext dbContext, 
        ILoggerFactory loggerFactory) : base(dbContext, loggerFactory)
    {
    }

    public async Task AddAsync(ProductionOrder productionOrder)
    {
        await InsertAsync(productionOrder);
    }

    public async Task EditAsync(ProductionOrder productionOrder)
    {
        await UpdateAsync(productionOrder);
    }

    public async Task<ProductionOrder?> GetByOderIdAsync(Guid orderId)
    {
        return await FindByPredicateAsync(product => product.Id.Equals(orderId));
    }

    public async Task<IEnumerable<ProductionOrder>> ListAllAsync()
    {
        return await ListByPredicateAsync(px => true);
    }

    public async Task<IEnumerable<ProductionOrder>> ListCurrentAsync()
    {
        throw new NotImplementedException();
    }
}