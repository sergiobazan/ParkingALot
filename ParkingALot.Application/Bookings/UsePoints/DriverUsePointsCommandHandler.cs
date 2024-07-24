using ParkingALot.Application.Abstractions.Messaging;
using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.Bookings;
using ParkingALot.Domain.Drivers;
using ParkingALot.Domain.ParkingLotOwners;

namespace ParkingALot.Application.Bookings.UsePoints;
internal sealed class DriverUsePointsCommandHandler(
    IBookingRepository bookingRepository,
    IDriversRepository driversRepository,
    IParkingLotRepository parkingLotRepository,
    PriceService priceService,
    IUnitOfWork unitOfWork) : ICommandHandler<DriverUsePointsCommand>
{
    public async Task<Result> Handle(DriverUsePointsCommand request, CancellationToken cancellationToken)
    {
        var booking = await bookingRepository.GetByIdAsync(request.BookingId);

        if (booking is null)
        {
            return Result.Failure(BookingErrors.NotFound);
        }

        var parkingLot = await parkingLotRepository.GetByIdAsync(booking.ParkingLotId);

        if (parkingLot is null)
        {
            return Result.Failure(ParkingLotOwnerErrors.ParkingLotNotFound);
        }

        var driver = await driversRepository.GetByIdAsync(booking.DriverId);

        if (driver is null)
        {
            return Result.Failure(DriverErrors.DriverNotFound);
        }

        driver.UsePoints(request.Points);

        booking.RecalculatePrices(parkingLot, priceService, true, request.Points);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
