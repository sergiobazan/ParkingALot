using ParkingALot.Application.Abstractions.Messaging;
using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.Drivers;

namespace ParkingALot.Application.Drivers.AddVehicle;

internal sealed class DriverAddVehicleCommandHandler : ICommandHandler<DriverAddVehicleCommand, Guid>
{
    private readonly IDriversRepository _driversRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DriverAddVehicleCommandHandler(IDriversRepository driversRepository, IUnitOfWork unitOfWork)
    {
        _driversRepository = driversRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DriverAddVehicleCommand request, CancellationToken cancellationToken)
    {
        var driver = await _driversRepository.GetByIdAsync(request.DriverId);

        if (driver is null)
        {
            return Result.Failure<Guid>(DriverErrors.DriverNotFound);
        }

        Result<Vehicle> vehicleResult = driver.AddVehicle(
            new Brand(request.Brand), 
            new Model(request.Model), 
            request.Year);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return vehicleResult.Value.Id;
    }
}
