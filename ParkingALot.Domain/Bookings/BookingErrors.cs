using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Domain.Bookings;

public static class BookingErrors
{
    public static readonly Error InvalidDateRange = new(
        "Booking.InvalidDateRange", "End date precedes start date");

    public static readonly Error NotFound = new(
        "Booking.NotFound", "Booking with given Id not found");

    public static readonly Error Overlap = new(
        "Booking.Overlap", "The current booking is overlapping with an existing one");
}
