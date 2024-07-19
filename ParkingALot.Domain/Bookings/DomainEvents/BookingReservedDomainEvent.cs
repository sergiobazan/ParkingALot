using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Domain.Bookings.DomainEvents;

public sealed record BookingReservedDomainEvent(Guid BookingId) : IDomainEvent;
