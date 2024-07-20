namespace ParkingALot.Domain.Drivers;

public interface IDriversRepository
{
    void Add(Driver driver);
    Task<Driver?> GetByIdAsync(Guid id);
}
