using ParkingALot.Domain.Bookings;

namespace ParkingALot.Infrastructure.Repositories;
internal class BookingRepository(ApplicationDbContext context)
    : Repository<Booking>(context), IBookingRepository
{
}
