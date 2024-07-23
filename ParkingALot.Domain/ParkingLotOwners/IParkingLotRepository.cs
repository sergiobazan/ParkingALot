namespace ParkingALot.Domain.ParkingLotOwners;

public interface IParkingLotRepository
{
    void Add(ParkingLot parkingLot);
    Task<ParkingLot> GetByIdAsync(Guid parkingLotId);
}
