namespace ParkingALot.Application.Bookings.SearchBooking;

public sealed record ParkingLotResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string Currency,
    AddressResponse Address);