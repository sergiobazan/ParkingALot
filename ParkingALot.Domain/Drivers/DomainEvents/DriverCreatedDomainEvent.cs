using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Domain.Drivers.DomainEvents;

public sealed record DriverCreatedDomainEvent(Guid DriverId) : IDomainEvent;

