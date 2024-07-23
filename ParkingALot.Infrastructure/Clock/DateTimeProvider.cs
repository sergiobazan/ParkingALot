using ParkingALot.Application.Abstractions.Clock;

namespace ParkingALot.Infrastructure.Clock;
internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
