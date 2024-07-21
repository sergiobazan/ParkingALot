using ParkingALot.Application.Abstractions.Messaging;
using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.ParkingLotOwners;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Application.ParkingLotOwners.Register;

internal sealed class RegisterParkingLotOwnerCommandHandler : ICommandHandler<RegisterParkingLotOwnerCommand, Guid>
{
    private readonly IParkingLotOwnerRepository _parkingLotOwnerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterParkingLotOwnerCommandHandler(IParkingLotOwnerRepository parkingLotOwnerRepository, IUnitOfWork unitOfWork)
    {
        _parkingLotOwnerRepository = parkingLotOwnerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterParkingLotOwnerCommand request, CancellationToken cancellationToken)
    {
        var parkingLotOwner = ParkingLotOwner.Create(
            new Name(request.Name),
            new Email(request.Email));

        _parkingLotOwnerRepository.Add(parkingLotOwner.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return parkingLotOwner.Value.Id;
    }
}
