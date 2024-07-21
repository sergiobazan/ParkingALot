using FluentValidation;

namespace ParkingALot.Application.ParkingLotOwners.Register;

internal sealed class RegisterParkingLotOwnerCommandValidator : AbstractValidator<RegisterParkingLotOwnerCommand>
{
    public RegisterParkingLotOwnerCommandValidator()
    {
        RuleFor(c => c.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Name)
            .NotNull()
            .NotEmpty();
    }
}
