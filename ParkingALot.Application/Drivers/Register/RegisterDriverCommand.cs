using ParkingALot.Application.Abstractions.Messaging;

namespace ParkingALot.Application.Drivers.Register;

public sealed record RegisterDriverCommand(string Name, string Email) : ICommand<Guid>;