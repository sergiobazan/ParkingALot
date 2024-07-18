using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Domain.Drivers;

public sealed class Driver : Entity
{
    private readonly List<Vehicle> _vehicles = new();
    private const int PointsPerHour = 20;
    private static readonly List<int> PointsValidForDiscount = new()
    {
        200,
        300
    };

    private Driver(
        Guid id,
        Name name,
        Email email,
        Point totalPoints) : base(id)
    {
        Name = name;
        Email = email;
        TotalPoints = totalPoints;
    }

    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Point TotalPoints { get; private set; }
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
        TotalPoints += Point.Create(PointsPerHour * totalHours);
    }

    public void UsePoints(int points)
    {
        if (!PointsValidForDiscount.Contains(points))
        {
            return;
        }

        TotalPoints -= Point.Create(points);
    }
}
