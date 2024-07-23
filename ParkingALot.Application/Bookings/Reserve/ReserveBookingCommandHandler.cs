using ParkingALot.Application.Abstractions.Clock;
using ParkingALot.Application.Abstractions.Messaging;
using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.Bookings;
using ParkingALot.Domain.Drivers;
using ParkingALot.Domain.ParkingLotOwners;

namespace ParkingALot.Application.Bookings.Reserve;
internal sealed class ReserveBookingCommandHandler(
    IDriversRepository driverRepository,
    IParkingLotRepository parkingLotRepository,
    IBookingRepository bookingRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork,
    PriceService priceService)
    : ICommandHandler<ReserveBookingCommand, Guid>
{
    public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
    {
        var driver = await driverRepository.GetByIdAsync(request.DriverId);

        if (driver is null)
        {
            return Result.Failure<Guid>(DriverErrors.DriverNotFound);
        }

        var parkingLot = await parkingLotRepository.GetByIdAsync(request.ParkingLotId);

        if (parkingLot is null)
        {
            return Result.Failure<Guid>(ParkingLotOwnerErrors.ParkingLotNotFound);
        }
        
        var range = DateRange.Create(request.Start, request.End);

        if (range.IsFailure)
        {
            return Result.Failure<Guid>(BookingErrors.InvalidDateRange);
        }

        var booking = Booking.Reserve(
            driver,
            parkingLot,
            priceService,
            range.Value,
            dateTimeProvider.UtcNow);

        bookingRepository.Add(booking.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return booking.Value.Id;
    }
}
