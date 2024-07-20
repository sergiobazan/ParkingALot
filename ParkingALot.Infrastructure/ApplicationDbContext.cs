using Microsoft.EntityFrameworkCore;
using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Infrastructure;

internal class ApplicationDbContext : DbContext, IUnitOfWork
{
    protected ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
