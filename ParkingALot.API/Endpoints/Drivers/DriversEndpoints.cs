using MediatR;
using ParkingALot.API.Endpoints.Drivers.Requests;
using ParkingALot.Application.Drivers.AddVehicle;
using ParkingALot.Application.Drivers.GetDriver;
using ParkingALot.Application.Drivers.Register;

namespace ParkingALot.API.Endpoints.Drivers;

public static class DriversEndpoints
{
    public static void AddDriversEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("drivers/register", async (RegisterDriverRequest request, ISender sender) =>
        {
            var command = new RegisterDriverCommand(request.Name, request.Email);

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Created($"drivers/${result.Value}", result.Value);
        });

        app.MapPost("drivers/vehicles", async (DriverAddVehicleRequest request, ISender sender) =>
        {
            var command = new DriverAddVehicleCommand(request.DriverId, request.Brand, request.Model, request.Year);

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Created($"drivers/vehicles/{result.Value}", result.Value);
        });

        app.MapGet("drivers/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetDriverQuery(id);

            var result = await sender.Send(query);

            return result.IsFailure ? Results.NotFound(result.Error) : Results.Ok(result.Value);
        });
    }
}
