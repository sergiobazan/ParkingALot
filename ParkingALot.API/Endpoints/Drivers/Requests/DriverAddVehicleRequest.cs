namespace ParkingALot.API.Endpoints.Drivers.Requests;

public sealed record DriverAddVehicleRequest(
    Guid DriverId,
    string Brand,
    string Model,
    DateOnly Year);

