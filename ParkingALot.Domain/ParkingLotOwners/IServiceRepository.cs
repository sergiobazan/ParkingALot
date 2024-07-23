namespace ParkingALot.Domain.ParkingLotOwners;

public interface IServiceRepository
{
    void Add(Service service);
    Task<Service?> GetByIdAsync(Guid serviceId);
}
