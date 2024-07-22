using ParkingALot.Domain.ParkingLotOwners;

namespace ParkingALot.Infrastructure.Repositories;

internal sealed class ServiceRepository(ApplicationDbContext context)
    : Repository<Service>(context), IServiceRepository
{
}
