using Microsoft.EntityFrameworkCore;
using ParkingALot.Application.Abstractions.DbContext;
using ParkingALot.Application.Abstractions.Messaging;
using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.Drivers;

namespace ParkingALot.Application.Drivers.GetDriver;
internal sealed class GetDriverQueryHandler(IApplicationDbContext context) 
    : IQueryHandler<GetDriverQuery, DriverResponse>
{
    public async Task<Result<DriverResponse>> Handle(GetDriverQuery request, CancellationToken cancellationToken)
    {
        var driver = await context
            .Drivers
            .AsNoTracking()
            .Where(driver => driver.Id == request.DriverId)
            .Select(driver => new DriverResponse(
                driver.Id,
                driver.Name.Value,
                driver.Email.Value,
                driver.Vehicles.Select(vehicle => new VehicleResponse(
                    vehicle.Id,
                    vehicle.Characteristics.Brand,
                    vehicle.Characteristics.Model,
                    vehicle.Characteristics.Year
                )).ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (driver is null)
        {
            return Result.Failure<DriverResponse>(DriverErrors.DriverNotFound);
        }

        return driver;
    }
}
