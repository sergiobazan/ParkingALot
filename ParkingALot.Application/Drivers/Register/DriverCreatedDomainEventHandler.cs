using MediatR;
using ParkingALot.Application.Abstractions.Email;
using ParkingALot.Domain.Drivers;
using ParkingALot.Domain.Drivers.DomainEvents;

namespace ParkingALot.Application.Drivers.Register;

internal class DriverCreatedDomainEventHandler : INotificationHandler<DriverCreatedDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly IDriversRepository _driversRepository;

    public DriverCreatedDomainEventHandler(IEmailService emailService, IDriversRepository driversRepository)
    {
        _emailService = emailService;
        _driversRepository = driversRepository;
    }

    public async Task Handle(DriverCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var driver = await _driversRepository.GetByIdAsync(notification.DriverId);

        if (driver is null)
        {
            return;
        }

        await _emailService.SendEmailAsync(driver.Email.Value, "Welcome to ParkingALot", "You are now regirested to ParkingALot");
    }
}
