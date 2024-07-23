using MediatR;
using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.Bookings;
using ParkingALot.Domain.Bookings.DomainEvents;
using ParkingALot.Domain.ParkingLotOwners;

namespace ParkingALot.Application.Bookings.AddService;
internal sealed class ServiceAddedToBookingDomainEventHandler(
    IBookingRepository bookingRepository,
    IParkingLotRepository parkingLotRepository,
    PriceService priceService,
    IUnitOfWork unitOfWork)
    : INotificationHandler<ServiceAddedToBookingDomainEvent>
{
    public async Task Handle(ServiceAddedToBookingDomainEvent notification, CancellationToken cancellationToken)
    {
        var booking = await bookingRepository.GetByIdAsync(notification.BookingId);

        if (booking is null)
        {
            return;
        }

        var parkingLot = await parkingLotRepository.GetByIdAsync(notification.ParkingLotId);

        if (parkingLot is null)
        {
            return;
        }

        booking.RecalculatePrices(parkingLot, priceService);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
