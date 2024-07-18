namespace ParkingALot.Domain.ParkingLotOwner;

public sealed record Currency(string Code)
{
    internal static readonly Currency None = new("");
    private static readonly Currency Usd = new("USD");
    private static readonly Currency Pen = new("PEN");
    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(c => c.Code == code) ??
            throw new ApplicationException("The currency code is invalid");
    }

    public static readonly IReadOnlyCollection<Currency> All = new[]
    {
        Usd,
        Pen,
    };
}