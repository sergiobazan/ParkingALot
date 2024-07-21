﻿using FluentValidation;

namespace ParkingALot.Application.Drivers.Register;

public class RegisterDriverCommandValidator : AbstractValidator<RegisterDriverCommand>
{
    public RegisterDriverCommandValidator()
    {
        RuleFor(c => c.Name)
            .MinimumLength(3)
            .NotNull()
            .NotEmpty();

        RuleFor(c => c.Email)
            .EmailAddress()
            .NotNull()
            .NotEmpty();
    }
}
