using Microsoft.EntityFrameworkCore;
using ParkingALot.Domain.Drivers;

namespace ParkingALot.Infrastructure.Repositories;

internal class DriverRepository : IDriversRepository
{
    private readonly ApplicationDbContext _context;

    public DriverRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Driver driver)
    {
        _context.Set<Driver>().Add(driver);
    }

    public async Task<Driver?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Driver>().FirstOrDefaultAsync(driver => driver.Id == id);
    }
}
