using MediatR;
using ParkingALot.API.Endpoints.ParkingLotOwners.Requests;
using ParkingALot.Application.ParkingLotOwners.AddService;
using ParkingALot.Application.ParkingLotOwners.CreateParkingLot;
using ParkingALot.Application.ParkingLotOwners.Register;

namespace ParkingALot.API.Endpoints.ParkingLotOwners;

public static class ParkingLotOwnersEndpoints
{
    public static void AddParkingLotOwnerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("parking-lot-owners/register", async (RegisterParkingLotOwnerRequest request, ISender sender) =>
        {
            var command = new RegisterParkingLotOwnerCommand(request.Name, request.Email);

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Created($"parking-lot-owners/{result.Value}", result.Value);
        });

        app.MapPost("parking-lot-owners/parking-lot", async (CreateParkingLotRequest request, ISender sender) =>
        {
            var command = new CreateParkingLotCommand(
                request.ParkingLotOwnerId,
                request.Name, 
                request.Description,
                request.Country,
                request.State,
                request.City,
                request.Street,
                request.Amount,
                request.CurrencyCode,
                request.OpenAtUtc,
                request.CloseAtUtc);

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Created($"parking-lot-owners/parking-lot/{result.Value}", result.Value);
        });

        app.MapPost("parking-lot-owners/parking-lot/service", async (AddServiceRequest request, ISender sender) =>
        {
            var command = new AddServiceCommand(
                request.OwnerId,
                request.ParkingLotId,
                request.Name,
                request.Description,
                request.Amount,
                request.Code,
                request.ImageUrl);

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Created($"parking-lot-owners/parking-lot/service/{result.Value}", result.Value);
        });
    }
}
