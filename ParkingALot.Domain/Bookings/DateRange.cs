using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Domain.Bookings;

public sealed record DateRange
{
    private DateRange() { }

    public DateTime Start { get; init; }
    public DateTime End {  get; init; }
    public int LengthInHours => (int)(End - Start).TotalHours;

    public static Result<DateRange> Create(DateTime start, DateTime end)
    {
        if (start > end)
        {
            return Result.Failure<DateRange>(BookingErrors.InvalidDateRange);
        }

        var dateRange = new DateRange()
        {
            Start = start,
            End = end,
        };

        return dateRange;
    }
}