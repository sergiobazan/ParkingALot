using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Domain.Drivers;

public sealed class Vehicle : Entity
{
    internal Vehicle(
        Guid id,
        Guid driverId,
        Brand brand,
        Model model,
        DateOnly year) : base(id)
    {
        DriverId = driverId;
        Brand = brand;
        Model = model;
        Year = year;
    }

    public Guid DriverId { get; private set; }
    public Brand Brand { get; private set; }
    public Model Model { get; private set; }
    public DateOnly Year { get; private set; }
}
