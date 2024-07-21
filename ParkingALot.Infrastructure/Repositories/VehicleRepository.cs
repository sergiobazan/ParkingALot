using ParkingALot.Domain.Drivers;

namespace ParkingALot.Infrastructure.Repositories;

internal class VehicleRepository(ApplicationDbContext context) 
    : Repository<Vehicle>(context), IVehicleRepository
{

}
