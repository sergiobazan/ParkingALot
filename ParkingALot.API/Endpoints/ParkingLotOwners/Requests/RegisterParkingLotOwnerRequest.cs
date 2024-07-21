namespace ParkingALot.API.Endpoints.ParkingLotOwners.Requests;

public sealed record RegisterParkingLotOwnerRequest(
    string Name,
    string Email);
