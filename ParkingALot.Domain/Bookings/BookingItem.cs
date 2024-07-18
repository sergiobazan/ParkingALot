using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Domain.Bookings;

public sealed class BookingItem : Entity
{
    internal BookingItem(Guid id, Guid bookingId, Guid serviceId, Money price) : base(id)
    {
        BookingId = bookingId;
        ServiceId = serviceId;
        Price = price;
    }

    public Guid BookingId { get;  private set; }
    public Guid ServiceId { get; private set; }
    public Money Price { get; private set; }
}