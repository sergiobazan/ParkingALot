using ParkingALot.Domain.ParkingLotOwners;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Domain.Bookings;

public class PriceService
{
    public PricingDetails GetTotalPrice(
        ParkingLot parkingLot,
        DateRange range,
        IReadOnlyList<BookingItem> bookingItems,
        bool usePoints,
        int points)
    {
        var currency = parkingLot.PricePerHour.Currency;

        var priceForPeriod = parkingLot.PricePerHour * range.LengthInHours;

        var servicesPrice = Money.Zero(currency);

        foreach (BookingItem item in bookingItems)
        {
            servicesPrice += item.Price;
        }

        var totalAmount = Money.Zero(currency);
        
        totalAmount += priceForPeriod;

        if (!servicesPrice.IsZero())
        {
            totalAmount += servicesPrice;
        }

        var totalDiscount = Money.Zero(currency);

        if (usePoints)
        {
            decimal discountPercentage = points switch
            {
                200 => 0.2m,
                300 => 0.3m,
                _ => 0
            };

            if (discountPercentage > 0)
            {
                totalDiscount = new Money(totalAmount.Amount * discountPercentage, currency);
                
                totalAmount -= totalDiscount;
            }
        }

        return new(priceForPeriod, servicesPrice, totalDiscount, totalAmount);
    }
}
