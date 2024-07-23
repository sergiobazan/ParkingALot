using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingALot.API.Endpoints.Bookings.Requests;
using ParkingALot.Application.Bookings.AddService;
using ParkingALot.Application.Bookings.Reserve;

namespace ParkingALot.API.Endpoints.Bookings;

public static class BookingEndpoints
{
    public static void AddBookinEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("bookings/reserve", async (ReserveBookingRequest request, ISender sender) =>
        {
            var command = new ReserveBookingCommand(
                request.DriverId,
                request.ParkingLotId,
                request.Start,
                request.End);

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Created($"bookings/{result.Value}", result.Value);
        });

        app.MapPost("bookings/{id}/item", async (Guid bookingId, AddServiceBookingRequest request, ISender sender) =>
        {
            var command = new AddServiceBookingCommand(
                bookingId,
                request.ServiceId);

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Created();
        });
    }
}
