using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Domain.Drivers;

public sealed class Driver : Entity
{
    private readonly List<Vehicle> _vehicles = new();

    public Driver(Guid id, Name name, Email email) : base(id)
    {
        Name = name;
        Email = email;
    }

    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public IReadOnlyList<Vehicle> Vehicles => _vehicles.ToList();

    public static Driver Create(Name name, Email email)
    {
        var driver = new Driver(Guid.NewGuid(), name, email);

        return driver;
    }

    public void AddVehicle(Brand brand, Model model, DateOnly year)
    {
        var vehicle = new Vehicle(Guid.NewGuid(), Id, brand, model, year);

        _vehicles.Add(vehicle);
    }
}
