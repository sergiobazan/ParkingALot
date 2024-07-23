using ParkingALot.Application.Abstractions.Messaging;

namespace ParkingALot.Application.Bookings.AddService;
public sealed record AddServiceBookingCommand(
    Guid BookingId,
    Guid ServicesId) : ICommand;
