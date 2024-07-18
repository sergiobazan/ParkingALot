using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Domain.Drivers;

public sealed class Driver : Entity
{
    private readonly List<Vehicle> _vehicles = new();
    private const int PointsPerHour = 20;

    private Driver(
        Guid id,
        Name name,
        Email email,
        Point total) : base(id)
    {
        Name = name;
        Email = email;
        Total = total;
    }

    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Point Total { get; private set; }
    public IReadOnlyList<Vehicle> Vehicles => _vehicles.ToList();

    public static Driver Create(Name name, Email email)
    {
        var driver = new Driver(Guid.NewGuid(), name, email, Point.Zero());

        return driver;
    }

    public void AddVehicle(Brand brand, Model model, DateOnly year)
    {
        var vehicle = new Vehicle(Guid.NewGuid(), Id, brand, model, year);

        _vehicles.Add(vehicle);
    }

    public void AddPoints(int totalHours)
    {
        Total += Point.Create(PointsPerHour * totalHours);
    }
}
