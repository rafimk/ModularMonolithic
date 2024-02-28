namespace Domain.Primitives;

public abstract class Entity<TEntityId> : IEquatable<Entity<TEntityId>>, IEntity
    where TEntityId : class, IEntityId
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity(TEntityId id)
        : this() =>
        Id = id ?? throw new ArgumentException("The entity identifier is required.", nameof(id));


    protected Entity()
    {
    }

    public TEntityId Id { get; private init; }

    public static bool operator ==(Entity<TEntityId>? a, Entity<TEntityId>? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Entity<TEntityId>? a, Entity<TEntityId>? b) => !(a == b);

    public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

    public virtual bool Equals(Entity<TEntityId>? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }

        return ReferenceEquals(this, other) || Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj is not Entity<TEntityId> other)
        {
            return false;
        }

        return Id == other.Id;
    }

    public override int GetHashCode() => Id.GetHashCode() * 41;

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}