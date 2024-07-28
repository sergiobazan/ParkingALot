using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Domain.Drivers;

public sealed class Vehicle : Entity
{
    private Vehicle() { }
    internal Vehicle(
        Guid id,
        Guid driverId,
        Details characteristics) : base(id)
    {
        DriverId = driverId;
        Characteristics = characteristics;
    }

    public Guid DriverId { get; private set; }
    public Details Characteristics { get; set; }
}
