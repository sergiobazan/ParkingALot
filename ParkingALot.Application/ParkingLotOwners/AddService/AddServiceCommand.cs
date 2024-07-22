using ParkingALot.Application.Abstractions.Messaging;

namespace ParkingALot.Application.ParkingLotOwners.AddService;

public sealed record AddServiceCommand(
    Guid OwnerId,
    Guid ParkingLotId,
    string Name,
    string Description,
    decimal Amount,
    string Code,
    string ImageUrl) : ICommand<Guid>;
