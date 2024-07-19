using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Domain.ParkingLotOwners.DomainEvents;

public sealed record ParkingLotCreatedDomainEvent(Guid ParkingLotId) : IDomainEvent;
