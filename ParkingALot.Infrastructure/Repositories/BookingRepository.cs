using Microsoft.EntityFrameworkCore;
using ParkingALot.Domain.Bookings;
using ParkingALot.Domain.ParkingLotOwners;

namespace ParkingALot.Infrastructure.Repositories;
internal class BookingRepository(ApplicationDbContext context)
    : Repository<Booking>(context), IBookingRepository
{
    private readonly IEnumerable<BookingStatus> ActiveBookingStatuses =
    [
        BookingStatus.Completed,
        BookingStatus.Reserved
    ];
    public override async Task<Booking?> GetByIdAsync(Guid id)
    {
        return await _context
            .Set<Booking>()
            .AsSplitQuery()
            .Include(booking => booking.BookingItems)
            .FirstOrDefaultAsync(booking => booking.Id == id);
    }

    public async Task<bool> IsOverlaping(ParkingLot parkingLot, DateRange duration, CancellationToken cancellationToken = default)
    {
        return await _context
            .Set<Booking>()
            .AnyAsync(booking => 
                booking.ParkingLotId == parkingLot.Id &&
                booking.Range.Start <= duration.End &&
                booking.Range.End >= duration.Start &&
                ActiveBookingStatuses.Contains(booking.Status),
                cancellationToken);
    }
}
