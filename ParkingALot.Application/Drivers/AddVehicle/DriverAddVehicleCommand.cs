using ParkingALot.Application.Abstractions.Messaging;

namespace ParkingALot.Application.Drivers.AddVehicle;

public sealed record DriverAddVehicleCommand(
    Guid DriverId,
    string Brand,
    string Model,
    int Year) : ICommand<Guid>;
