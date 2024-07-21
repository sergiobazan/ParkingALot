using ParkingALot.Application.Abstractions.Messaging;

namespace ParkingALot.Application.ParkingLotOwners.CreateParkingLot;

public sealed record CreateParkingLotCommand(
    Guid ParkingLotOwnerId,
    string Name,
    string Description,
    string Country,
    string State,
    string City,
    string Street,
    decimal Amount,
    string CurrencyCode,
    DateTime OpenAtUtc,
    DateTime CloseAtUtc) : ICommand<Guid>;
