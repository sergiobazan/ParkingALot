namespace ParkingALot.Application.Bookings.SearchBooking;

public sealed record AddressResponse(
    string Country,
    string State,
    string City,
    string Street);