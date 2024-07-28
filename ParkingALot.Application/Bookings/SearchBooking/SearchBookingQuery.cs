using ParkingALot.Application.Abstractions.Messaging;

namespace ParkingALot.Application.Bookings.SearchBooking;
public sealed record SearchBookingQuery(DateTime Start, DateTime End) 
    : IQuery<IReadOnlyList<ParkingLotResponse>>;
