using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Domain.ParkingLotOwners.DomainEvents;

public sealed record ParkingLotOwnerCreatedDomainEvent(Guid ParkingLotOwner) : IDomainEvent;
