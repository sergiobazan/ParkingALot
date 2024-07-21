using ParkingALot.Domain.Drivers;

namespace ParkingALot.Infrastructure.Repositories;

internal class DriverRepository(ApplicationDbContext context) 
    : Repository<Driver>(context), IDriversRepository
{

}
