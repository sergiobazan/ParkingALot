namespace ParkingALot.Domain.ParkingLotOwner;

public sealed record Address(
    string Country,
    string State,
    string City,
    string Street);