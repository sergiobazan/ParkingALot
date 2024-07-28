using ParkingALot.Application.Abstractions.Messaging;

namespace ParkingALot.Application.Drivers.GetDriver;
public sealed record GetDriverQuery(Guid DriverId) : IQuery<DriverResponse>;

public sealed record DriverResponse(
    Guid Id,
    string Name,
    string Email,
    IReadOnlyList<VehicleResponse> Vehicles);

public sealed record VehicleResponse(
    Guid Id,
    string Brand,
    string Model,
    int Year);