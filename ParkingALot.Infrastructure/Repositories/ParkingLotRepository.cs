using Microsoft.EntityFrameworkCore;
using ParkingALot.Domain.ParkingLotOwners;

namespace ParkingALot.Infrastructure.Repositories;

internal sealed class ParkingLotRepository(ApplicationDbContext context) 
    : Repository<ParkingLot>(context), IParkingLotRepository
{
    public override async Task<ParkingLot?> GetByIdAsync(Guid id)
    {
        return await _context
            .Set<ParkingLot>()
            .AsSplitQuery()
            .Include(booking => booking.Services)
            .FirstOrDefaultAsync(booking => booking.Id == id);
    }
}
