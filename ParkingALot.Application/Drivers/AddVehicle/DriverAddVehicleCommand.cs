﻿using ParkingALot.Application.Abstractions.Messaging;

namespace ParkingALot.Application.Drivers.AddVehicle;

public sealed record DriverAddVehicleCommand(
    Guid DriverId,
    string Brand,
    string Model,
    DateOnly Year) : ICommand<Guid>;