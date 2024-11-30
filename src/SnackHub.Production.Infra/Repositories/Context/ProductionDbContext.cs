using Microsoft.EntityFrameworkCore;
using SnackHub.Production.Infra.Repositories.Maps;

namespace SnackHub.Production.Infra.Repositories.Context;

public class ProductionDbContext : DbContext
{
    public ProductionDbContext(DbContextOptions<ProductionDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductionDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductionOrderItemMap());
    }
}