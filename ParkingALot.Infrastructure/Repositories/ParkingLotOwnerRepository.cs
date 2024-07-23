using Microsoft.EntityFrameworkCore;
using ParkingALot.Domain.ParkingLotOwners;

namespace ParkingALot.Infrastructure.Repositories;

internal sealed class ParkingLotOwnerRepository(ApplicationDbContext context) 
    : Repository<ParkingLotOwner>(context), IParkingLotOwnerRepository
{
    public override async Task<ParkingLotOwner?> GetByIdAsync(Guid id)
    {
        return await _context
            .Set<ParkingLotOwner>()
            .AsSplitQuery()
            .Include(owner => owner.ParkingLots)
            .FirstOrDefaultAsync(owner => owner.Id == id);
    }
}
