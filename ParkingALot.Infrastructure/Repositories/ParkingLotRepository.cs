using ParkingALot.Domain.ParkingLotOwners;

namespace ParkingALot.Infrastructure.Repositories;

internal sealed class ParkingLotRepository(ApplicationDbContext context) 
    : Repository<ParkingLot>(context), IParkingLotRepository
{
}
