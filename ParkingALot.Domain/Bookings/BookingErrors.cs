using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Domain.Bookings;

public static class BookingErrors
{
    public static readonly Error InvalidDateRange = new(
        "Booking.InvalidDateRange", "End date precedes start date");
}
