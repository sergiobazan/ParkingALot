namespace ParkingALot.API.Endpoints.Bookings.Requests;

public sealed record ReserveBookingRequest(
    Guid DriverId,
    Guid ParkingLotId,
    DateTime Start,
    DateTime End,
    bool UsePoints,
    int Points);