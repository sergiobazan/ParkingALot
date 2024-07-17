namespace ParkingALot.Domain.Abstractions;

public abstract class Entity
{
    protected Entity(Guid id)
    {
        Id = id;
    }
    protected Guid Id { get; init; }

}
