using ParkingALot.Domain.Drivers;

namespace ParkingALot.Infrastructure.Repositories;

internal class VehicleRepository : IVehicleRepository
{
    private readonly ApplicationDbContext _context;

    public VehicleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Vehicle vehicle)
    {
        _context.Set<Vehicle>().Add(vehicle);
    }
}
