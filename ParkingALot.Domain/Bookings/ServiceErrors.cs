using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Domain.Bookings;
public static class ServiceErrors
{
    public static readonly Error NotFound = new(
        "Service.NotFound", "Service with given Id not found");
}
