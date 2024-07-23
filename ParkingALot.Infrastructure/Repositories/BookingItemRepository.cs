using ParkingALot.Domain.Bookings;

namespace ParkingALot.Infrastructure.Repositories;
internal class BookingItemRepository : Repository<BookingItem>, IBookingItemRepository
{
    public BookingItemRepository(ApplicationDbContext context) : base(context)
    {
    }
}
