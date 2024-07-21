using FluentValidation;

namespace ParkingALot.Application.Drivers.AddVehicle;

public sealed class DriverAddVehicleCommandValidator : AbstractValidator<DriverAddVehicleCommand>
{
    public DriverAddVehicleCommandValidator()
    {
        RuleFor(c => c.DriverId)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Model)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Brand)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Year)
            .NotEmpty()
            .NotNull();
    }
}
