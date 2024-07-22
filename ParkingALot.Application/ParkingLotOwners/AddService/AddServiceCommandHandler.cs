using ParkingALot.Application.Abstractions.Messaging;
using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.ParkingLotOwners;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Application.ParkingLotOwners.AddService;

internal sealed class AddServiceCommandHandler : ICommandHandler<AddServiceCommand, Guid>
{
    private readonly IParkingLotOwnerRepository _parkingLotOwnerRepository;
    private readonly IParkingLotRepository _parkingLotRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddServiceCommandHandler(
        IParkingLotOwnerRepository parkingLotOwnerRepository,
        IParkingLotRepository parkingLotRepository,
        IServiceRepository serviceRepository,
        IUnitOfWork unitOfWork)
    {
        _parkingLotOwnerRepository = parkingLotOwnerRepository;
        _parkingLotRepository = parkingLotRepository;
        _serviceRepository = serviceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(AddServiceCommand request, CancellationToken cancellationToken)
    {
        var owner = await _parkingLotOwnerRepository.GetByIdAsync(request.OwnerId);

        if (owner is null)
        {
            return Result.Failure<Guid>(ParkingLotOwnerErrors.NotFound);
        }

        var parkingLot = owner.ParkingLots.FirstOrDefault(parking => parking.Id == request.ParkingLotId);

        if (parkingLot is null)
        {
            return Result.Failure<Guid>(ParkingLotOwnerErrors.ParkingLotNotFound);
        }

        Result<Service> service = parkingLot.AddService(
            new Name(request.Name),
            new Description(request.Description),
            new Money(
                request.Amount,
                Currency.FromCode(request.Code)),
            new Image(request.ImageUrl));

        _serviceRepository.Add(service.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return service.Value.Id;
    }
}
