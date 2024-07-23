﻿using ParkingALot.Application.Abstractions.Messaging;

namespace ParkingALot.Application.Bookings.Reserve;

public sealed record ReserveBookingCommand(
    Guid DriverId,
    Guid ParkingLotId,
    DateTime Start,
    DateTime End) : ICommand<Guid>;