namespace ParkingALot.Domain.Abstractions;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];
    protected Entity() { }
    protected Entity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }
    public IReadOnlyList<IDomainEvent> DomainEvents => [.. _domainEvents];

    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
