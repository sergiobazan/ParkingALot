using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingALot.API.Endpoints.Bookings.Requests;
using ParkingALot.Application.Bookings.AddService;
using ParkingALot.Application.Bookings.Reserve;
using ParkingALot.Application.Bookings.SearchBooking;
using ParkingALot.Application.Bookings.UsePoints;

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

        app.MapPost("bookings/{bookingId}/item", async (Guid bookingId, AddServiceBookingRequest request, ISender sender) =>
        {
            var command = new AddServiceBookingCommand(
                bookingId,
                request.ServiceId);

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok();
        });

        app.MapPost("bookings/drives/points", async (DriverUsePointsRequest request, ISender sender) =>
        {
            var command = new DriverUsePointsCommand(
                request.BookingId,
                request.Points);

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok();
        });

        app.MapGet("bookings/search", async (DateTime start, DateTime end, ISender sender) =>
        {
            var query = new SearchBookingQuery(start, end);

            var result = await sender.Send(query);

            return result.IsFailure ? Results.NotFound(result.Error) : Results.Ok(result.Value);
        });
    }
}
