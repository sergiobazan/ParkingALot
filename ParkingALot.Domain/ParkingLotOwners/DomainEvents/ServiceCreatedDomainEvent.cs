using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Domain.ParkingLotOwners.DomainEvents;

public sealed record ServiceCreatedDomainEvent(Guid ServiceId) : IDomainEvent;
