namespace SnackHub.Production.Domain.Base;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{

    protected Entity(TId id)
    {
        Id = id;
    }

    public virtual TId Id { get; }

    public virtual DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

    public virtual DateTime? UpdatedAt { get; protected set; } = DateTime.UtcNow;
    
    public bool Equals(Entity<TId>? other)
    {
        throw new NotImplementedException();
    }
}
