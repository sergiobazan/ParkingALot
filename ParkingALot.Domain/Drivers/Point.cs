namespace ParkingALot.Domain.Drivers;

public sealed record Point(int Total)
{
    public static Point Create(int total)
    {
       if (total < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(total), "Points can't be negative");
        }

       return new Point(total);
    }

    public static Point Zero() => new(0);

    public static Point operator +(Point first, Point second)
    {
        return new(first.Total + second.Total);
    }

    public static Point operator *(Point points, int multiplier)
    {
        if (multiplier < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(multiplier), "Multiplier can't be negative");
        }

        return new(points.Total * multiplier);
    }
}
