using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnackHub.Production.Domain.Entities;
using SnackHub.Production.Domain.ValueObjects;
using System.Collections.Generic;
using System.Text.Json;

namespace SnackHub.Production.Infra.Repositories.Maps;

public class ProductionOrderMap : IEntityTypeConfiguration<ProductionOrder>
{
    public void Configure(EntityTypeBuilder<ProductionOrder> builder)
    {
        builder
            .Property(px => px.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(px => px.OrderId)
            .HasColumnName("OrderId")
            .IsRequired();

        builder
           .Property(px => px.Status)
           .HasColumnName("Status")
           .IsRequired();

        builder
            .HasMany(px => px.Items)
            .WithOne()
            .HasForeignKey("ProductionOrderId")
            .IsRequired();
    }
}