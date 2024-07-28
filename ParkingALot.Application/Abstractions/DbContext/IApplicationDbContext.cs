using Microsoft.EntityFrameworkCore;
using ParkingALot.Domain.Bookings;
using ParkingALot.Domain.Drivers;
using ParkingALot.Domain.ParkingLotOwners;

namespace ParkingALot.Application.Abstractions.DbContext;
public interface IApplicationDbContext
{
    DbSet<Driver> Drivers { get; }
    DbSet<ParkingLotOwner> ParkingLotOwners { get; }
    DbSet<Booking> Bookings { get; }
}
