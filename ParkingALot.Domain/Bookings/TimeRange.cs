namespace ParkingALot.Domain.Bookings;

public sealed record TimeRange
{
    private TimeRange() { }

    public TimeOnly Start { get; init; }
    public TimeOnly End {  get; init; }
    public int LengthInHours => End.Hour - Start.Hour;

    public static TimeRange Create(TimeOnly start, TimeOnly end)
    {
        if (start > end)
        {
            throw new ApplicationException("End date precedes start date");
        }

        return new()
        {
            Start = start,
            End = end,
        };
    }
}