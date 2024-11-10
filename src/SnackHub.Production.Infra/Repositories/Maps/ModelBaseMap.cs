using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SnackHub.Production.Domain.Base;

namespace SnackHub.Production.Infra.Repositories.Maps;

public class ModelBaseMap<TModel, TId> : IEntityTypeConfiguration<TModel>
    where TModel : Entity<TId> 
    where TId : notnull
{
    public void Configure(EntityTypeBuilder<TModel> builder)
    {
        builder
            .Property(px => px.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(px => px.CreatedAt)
            .HasColumnName("CreatedAt")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(px => px.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .ValueGeneratedOnUpdate()
            .IsRequired();
    }
}