using MediatR;
using ParkingALot.API.Endpoints.ParkingLotOwners.Requests;
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
    }
}
