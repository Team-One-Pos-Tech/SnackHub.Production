using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SnackHub.Production.Domain.Contracts;
using SnackHub.Production.Domain.Entities;
using SnackHub.Production.Infra.Repositories.Context;

namespace SnackHub.Production.Infra.Repositories;

public class ProductionOrderRepository(
    ProductionDbContext dbContext,
    ILoggerFactory loggerFactory)
    : BaseRepository<ProductionOrder, ProductionDbContext>(
        dbContext, loggerFactory
        ), IProductionOrderRepository
{
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
        return await _dbSet
            .Include(px => px.Items)
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<IEnumerable<ProductionOrder>> ListCurrentAsync()
    {
        throw new NotImplementedException();
    }
}