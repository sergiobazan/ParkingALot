using FluentValidation;

namespace ParkingALot.Application.ParkingLotOwners.Register;

public sealed class RegisterParkingLotOwnerCommandValidator : AbstractValidator<RegisterParkingLotOwnerCommand>
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
