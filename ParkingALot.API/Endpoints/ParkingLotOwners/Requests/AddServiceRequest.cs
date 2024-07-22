namespace ParkingALot.API.Endpoints.ParkingLotOwners.Requests;

public sealed record AddServiceRequest(
    Guid OwnerId,
    Guid ParkingLotId,
    string Name,
    string Description,
    decimal Amount,
    string Code,
    string ImageUrl);