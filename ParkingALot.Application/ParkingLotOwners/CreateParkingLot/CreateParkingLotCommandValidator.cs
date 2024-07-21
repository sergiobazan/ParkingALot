using FluentValidation;

namespace ParkingALot.Application.ParkingLotOwners.CreateParkingLot;

public sealed class CreateParkingLotCommandValidator : AbstractValidator<CreateParkingLotCommand>
{
    public CreateParkingLotCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Description)
            .NotEmpty()
            .NotNull();

        RuleFor(c => c.Country)
            .NotNull()
            .NotEmpty();

        RuleFor(c => c.State)
           .NotNull()
           .NotEmpty();

        RuleFor(c => c.City)
           .NotNull()
           .NotEmpty();

        RuleFor(c => c.Street)
           .NotNull()
           .NotEmpty();

        RuleFor(c => c.Amount)
           .NotNull()
           .NotEmpty();

        RuleFor(c => c.CurrencyCode)
           .NotNull()
           .NotEmpty();
    }
}
