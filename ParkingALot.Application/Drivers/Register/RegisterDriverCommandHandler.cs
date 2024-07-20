using ParkingALot.Application.Abstractions.Messaging;
using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.Drivers;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Application.Drivers.Register;

internal sealed class RegisterDriverCommandHandler : ICommandHandler<RegisterDriverCommand, Guid>
{
    private readonly IDriversRepository _driversRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterDriverCommandHandler(IDriversRepository driversRepository, IUnitOfWork unitOfWork)
    {
        _driversRepository = driversRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterDriverCommand request, CancellationToken cancellationToken)
    {
        var driver = Driver.Create(new Name(request.Name), new Email(request.Email));

        _driversRepository.Add(driver.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return driver.Value.Id;
    }
}
