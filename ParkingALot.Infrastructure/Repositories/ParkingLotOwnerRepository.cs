using ParkingALot.Domain.ParkingLotOwners;

namespace ParkingALot.Infrastructure.Repositories;

internal sealed class ParkingLotOwnerRepository(ApplicationDbContext context) 
    : Repository<ParkingLotOwner>(context), IParkingLotOwnerRepository
{

}
