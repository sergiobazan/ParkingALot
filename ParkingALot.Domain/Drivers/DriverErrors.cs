﻿using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Domain.Drivers;

public static class DriverErrors
{
    public static readonly Error InvalidPoints = new(
        "Drivers.InvalidPoints", "Points can't be negative");
}
