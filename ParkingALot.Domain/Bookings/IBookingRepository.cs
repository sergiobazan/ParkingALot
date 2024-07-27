using ParkingALot.Domain.ParkingLotOwners;

namespace ParkingALot.Domain.Bookings;
public interface IBookingRepository
{
    void Add(Booking booking);
    Task<Booking?> GetByIdAsync(Guid id);
    Task<bool> IsOverlaping(ParkingLot parkingLot, DateRange duration, CancellationToken cancellationToken = default);
}
