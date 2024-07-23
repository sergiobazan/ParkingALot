namespace ParkingALot.Domain.Bookings;
public interface IBookingRepository
{
    void Add(Booking booking);
    Task<Booking?> GetByIdAsync(Guid id);
}
