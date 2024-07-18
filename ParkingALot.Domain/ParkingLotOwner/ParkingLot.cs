using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Domain.ParkingLotOwner;

public sealed class ParkingLot : Entity
{
    private readonly List<Service> _services = new();

    internal ParkingLot(
        Guid id,
        Guid parkingLotOwnerId,
        Name name,
        Description description,
        Address address,
        DateTime openAtUtc,
        DateTime closeAtUtc) : base(id)
    {
        ParkingLotOwnerId = parkingLotOwnerId;
        Name = name;
        Description = description;
        Address = address;
        OpenAtUtc = openAtUtc;
        CloseAtUtc = closeAtUtc;
    }

    public Guid ParkingLotOwnerId { get; private set; }
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public Address Address { get; private set; }
    public DateTime OpenAtUtc { get; private set; }
    public DateTime CloseAtUtc { get; private set; }
    public IReadOnlyList<Service> Services => _services.ToList();

    public void AddService(
        Name name,
        Description description,
        Money price,
        Image imageUrl)
    {
        var service = new Service(Guid.NewGuid(), Id, name, description, price, imageUrl);

        _services.Add(service);
    }
}
