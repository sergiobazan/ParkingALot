namespace ParkingALot.Domain.ParkingLotOwners;

public sealed record Address(
    string Country,
    string State,
    string City,
    string Street);