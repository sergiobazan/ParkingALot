using FluentValidation;
using MediatR;
using ParkingALot.Application.Abstractions.Messaging;
using ParkingALot.Application.Exceptions;

namespace ParkingALot.Application.Abstractions.Behaviors;

internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandBase
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(ICollection<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        List<ValidationError> errors = _validators
            .Select(validator => validator.Validate(context))
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationError => new ValidationError(
                validationError.ErrorCode,
                validationError.ErrorMessage))
            .ToList();

        if (errors.Count == 0)
        {
            return await next();
        }

        throw new Exceptions.ValidationException(errors);
    }
}
