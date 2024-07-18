namespace ParkingALot.Domain.Bookings;

public sealed record DateRange
{
    private DateRange() { }

    public DateTime Start { get; init; }
    public DateTime End {  get; init; }
    public int LengthInHours => (int)(End - Start).TotalHours;

    public static DateRange Create(DateTime start, DateTime end)
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