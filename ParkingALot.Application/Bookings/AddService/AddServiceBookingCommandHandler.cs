using ParkingALot.Application.Abstractions.Messaging;
using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.Bookings;
using ParkingALot.Domain.ParkingLotOwners;

namespace ParkingALot.Application.Bookings.AddService;
internal sealed class AddServiceBookingCommandHandler(
    IBookingRepository bookingRepository,
    IParkingLotRepository parkingLotRepository,
    IBookingItemRepository bookingItemRepository,
    IUnitOfWork unitOfWork,
    PriceService priceService) 
    : ICommandHandler<AddServiceBookingCommand>
{
    public async Task<Result> Handle(AddServiceBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await bookingRepository.GetByIdAsync(request.BookingId);

        if (booking is null)
        {
            return Result.Failure<Guid>(BookingErrors.NotFound);
        }

        var parkingLot = await parkingLotRepository.GetByIdAsync(booking.ParkingLotId);

        if (parkingLot is null)
        {
            return Result.Failure(ParkingLotOwnerErrors.ParkingLotNotFound);
        }

        var service = parkingLot.Services.FirstOrDefault(service => service.Id == request.ServicesId);

        if (service is null)
        {
            return Result.Failure(ServiceErrors.NotFound);
        }

        Result<BookingItem> bookingItem = booking.AddBookingItem(service);

        bookingItemRepository.Add(bookingItem.Value);

        booking.RecalculatePrices(parkingLot, priceService);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        // SECOND OPTION
        //await publisher.Publish(new ServiceAddedToBookingDomainEvent(
        //    booking.Id,
        //    parkingLot.Id),
        //    cancellationToken);

        return Result.Success();
    }
}
