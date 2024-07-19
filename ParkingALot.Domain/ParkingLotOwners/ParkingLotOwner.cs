using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.ParkingLotOwners.DomainEvents;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Domain.ParkingLotOwners;

public sealed class ParkingLotOwner : Entity
{
    private readonly List<ParkingLot> _parkingLots = new();
    private ParkingLotOwner(Guid id, Name name, Email email) : base(id)
    {
        Name = name;
        Email = email;
    }

    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public IReadOnlyList<ParkingLot> ParkingLots => _parkingLots.ToList();

    public static Result<ParkingLotOwner> Create(Name name, Email email)
    {
        var owner = new ParkingLotOwner(Guid.NewGuid(), name, email);

        owner.RaiseDomainEvent(new ParkingLotOwnerCreatedDomainEvent(owner.Id));

        return owner;
    }

    public void AddParkingLot(
        Name name,
        Description description,
        Address address,
        Money pricePerHour,
        DateTime openAtUtc,
        DateTime closeAtUtc)
    {
        var parkingLot = new ParkingLot(Guid.NewGuid(), Id, name, description, address, pricePerHour, openAtUtc, closeAtUtc);

        _parkingLots.Add(parkingLot);

        parkingLot.RaiseDomainEvent(new ParkingLotCreatedDomainEvent(parkingLot.Id));
    }
}
