using Microsoft.EntityFrameworkCore;
using ParkingALot.Application.Abstractions.DbContext;
using ParkingALot.Application.Abstractions.Messaging;
using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.Bookings;

namespace ParkingALot.Application.Bookings.SearchBooking;
internal sealed class SearchBookingQueryHandler(IApplicationDbContext context) 
    : IQueryHandler<SearchBookingQuery, IReadOnlyList<ParkingLotResponse>>
{
    private readonly IEnumerable<BookingStatus> ActiveBookingStatuses = 
    [
        BookingStatus.Completed,
        BookingStatus.Reserved
    ];
    public async Task<Result<IReadOnlyList<ParkingLotResponse>>> Handle(SearchBookingQuery request, CancellationToken cancellationToken)
    {
        var parkingLots = await context
            .ParkingLots
            .AsNoTracking()
            .Where(parkingLot => !context.Bookings.Where(booking => 
                    booking.ParkingLotId == parkingLot.Id &&
                    booking.Range.Start <= request.End &&
                    booking.Range.End >= request.Start &&
                    ActiveBookingStatuses.Contains(booking.Status)
                ).Any())
            .Select(parkingLot => new ParkingLotResponse(
                    parkingLot.Id,
                    parkingLot.Name.Value,
                    parkingLot.Description.Value,
                    parkingLot.PricePerHour.Amount,
                    parkingLot.PricePerHour.Currency.Code,
                    new AddressResponse(parkingLot.Address.Country, parkingLot.Address.State, parkingLot.Address.City, parkingLot.Address.Street)
                ))
            .ToListAsync(cancellationToken);

        return parkingLots;
    }
}
