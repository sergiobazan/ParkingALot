using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Domain.Bookings.DomainEvents;
public sealed record ServiceAddedToBookingDomainEvent(
    Guid BookingId,
    Guid ParkingLotId) : IDomainEvent;
