namespace ParkingALot.API.Endpoints.ParkingLotOwners.Requests;

public sealed record CreateParkingLotRequest(
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
    DateTime CloseAtUtc);
