using Microsoft.EntityFrameworkCore;
using ParkingALot.Application.Abstractions.DbContext;
using ParkingALot.Application.Exceptions;
using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.Bookings;
using ParkingALot.Domain.Drivers;
using ParkingALot.Domain.ParkingLotOwners;

namespace ParkingALot.Infrastructure;

public class ApplicationDbContext : DbContext, IUnitOfWork, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    public DbSet<Driver> Drivers { get; set; }
    public DbSet<ParkingLotOwner> ParkingLotOwners { get; set; }
    public DbSet<ParkingLot> ParkingLots { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency Exception ocurred.", ex);
        }
    }
}
