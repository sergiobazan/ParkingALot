﻿using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Domain.ParkingLotOwners;

public static class ParkingLotOwnerErrors
{
    public static readonly Error NotFound = new(
        "ParkingLotOwner.NotFound", "Parking lot owner with given Id not found");

    public static readonly Error ParkingLotNotFound = new(
        "ParkingLotOwner.ParkingLotNotFound", "Parking lot with given Id not found");
}
