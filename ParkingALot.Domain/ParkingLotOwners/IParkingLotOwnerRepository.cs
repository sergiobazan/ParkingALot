namespace ParkingALot.Domain.ParkingLotOwners;

public interface IParkingLotOwnerRepository
{
    void Add(ParkingLotOwner parkingLotOwner);
    Task<ParkingLotOwner?> GetByIdAsync(Guid parkingLotOwnerId);
}
