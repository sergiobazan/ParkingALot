using ParkingALot.Application.Abstractions.Messaging;

namespace ParkingALot.Application.Bookings.UsePoints;
public sealed record DriverUsePointsCommand(
    Guid BookingId,
    int Points) : ICommand;
