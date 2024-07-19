using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Domain.Drivers.DomainEvents;

public sealed record VehicleCreatedDomainEvent(Guid VehicleId) : IDomainEvent;
