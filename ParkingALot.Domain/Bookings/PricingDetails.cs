using ParkingALot.Domain.Shared;

namespace ParkingALot.Domain.Bookings;

public sealed record PricingDetails(
    Money PriceForPeriod,
    Money ServicesPrice,
    Money PointsDiscount,
    Money TotalPrice);