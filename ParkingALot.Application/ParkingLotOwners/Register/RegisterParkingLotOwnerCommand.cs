using ParkingALot.Application.Abstractions.Messaging;

namespace ParkingALot.Application.ParkingLotOwners.Register;

public sealed record RegisterParkingLotOwnerCommand(
    string Name,
    string Email) : ICommand<Guid>;
