namespace ParkingALot.API.Endpoints.Bookings.Requests;

public sealed record DriverUsePointsRequest(
    Guid BookingId,
    int Points);
