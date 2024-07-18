using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Domain.ParkingLotOwners;

public sealed class Service : Entity
{
    internal Service(
        Guid id,
        Guid parkingLotId,
        Name name,
        Description description,
        Money price,
        Image imageUrl) : base(id)
    {
        ParkingLotId = parkingLotId;
        Name = name;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
    }

    public Guid ParkingLotId { get; private set; }
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public Money Price { get; private set; }
    public Image ImageUrl { get; private set; }
}
