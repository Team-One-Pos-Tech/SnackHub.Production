using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnackHub.Production.Domain.Entities;

namespace SnackHub.Production.Infra.Repositories.Maps;

public class ProductionOrderItemMap : IEntityTypeConfiguration<ProductionOrderItem>
{
    public void Configure(EntityTypeBuilder<ProductionOrderItem> builder)
    {
        builder
            .Property(px => px.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(px => px.ProductId)
            .HasColumnName("ProductId")
            .IsRequired();

        builder
            .Property(px => px.Quantity)
            .HasColumnName("Quantity")
            .IsRequired();
    }
}