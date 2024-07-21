using ParkingALot.Application.Abstractions.Messaging;
using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.ParkingLotOwners;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Application.ParkingLotOwners.CreateParkingLot;

internal sealed class CreateParkingLotCommandHandler : ICommandHandler<CreateParkingLotCommand, Guid>
{
    private readonly IParkingLotRepository _parkingLotRepository;
    private readonly IParkingLotOwnerRepository _parkingLotOwnerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateParkingLotCommandHandler(IParkingLotRepository parkingLotRepository, IParkingLotOwnerRepository parkingLotOwnerRepository, IUnitOfWork unitOfWork)
    {
        _parkingLotRepository = parkingLotRepository;
        _parkingLotOwnerRepository = parkingLotOwnerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateParkingLotCommand request, CancellationToken cancellationToken)
    {
        var owner = await _parkingLotOwnerRepository.GetByIdAsync(request.ParkingLotOwnerId);

        if (owner is null)
        {
            return Result.Failure<Guid>(ParkingLotOwnerErrors.NotFound);
        }

        Result<ParkingLot> parkingLot = owner.AddParkingLot(
            new Name(request.Name),
            new Description(request.Description),
            new Address(
                request.Country,
                request.State,
                request.City,
                request.Street),
            new Money(
                request.Amount, 
                Currency.FromCode(request.CurrencyCode)),
            request.OpenAtUtc,
            request.CloseAtUtc);

        _parkingLotRepository.Add(parkingLot.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return parkingLot.Value.Id;
    }
}
