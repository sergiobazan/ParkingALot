namespace ParkingALot.Application.Exceptions;

public class ValidationException(IReadOnlyCollection<ValidationError> errors) 
    : Exception("Validation failed")
{
    public IReadOnlyCollection<ValidationError> Errors { get; init; } = errors;
}


public sealed record ValidationError(string Code, string Message);