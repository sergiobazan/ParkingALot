namespace ParkingALot.Domain.Shared;

public sealed record Money(decimal Amount, Currency Currency)
{
    public static Money operator +(Money first, Money second)
    {
        if (first.Currency != second.Currency)
        {
            throw new InvalidOperationException("Currencies have to be equal");
        }

        return new(first.Amount + second.Amount, first.Currency);
    }
    public static Money operator -(Money first, Money second)
    {
        if (first.Currency != second.Currency)
        {
            throw new InvalidOperationException("Currencies have to be equal");
        }

        return new(first.Amount - second.Amount, first.Currency);
    }
    public static Money operator *(Money money, decimal multiplier)
    {
        if (multiplier < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(multiplier), "Multiplier can't be negative");
        }

        return new(money.Amount * multiplier, money.Currency);
    }
    public static Money Zero() => new(0, Currency.None);
    public static Money Zero(Currency currency) => new(0, currency);
    public bool IsZero() => this == Zero(Currency);
}
