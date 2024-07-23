using Microsoft.EntityFrameworkCore;
using ParkingALot.Domain.Bookings;

namespace ParkingALot.Infrastructure.Repositories;
internal class BookingRepository(ApplicationDbContext context)
    : Repository<Booking>(context), IBookingRepository
{
    public override async Task<Booking?> GetByIdAsync(Guid id)
    {
        return await _context
            .Set<Booking>()
            .AsSplitQuery()
            .Include(booking => booking.BookingItems)
            .FirstOrDefaultAsync(booking => booking.Id == id);
    }
}
