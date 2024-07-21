using Microsoft.AspNetCore.Mvc;
using ParkingALot.Application.Exceptions;

namespace ParkingALot.API.Middlewares;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ValidationMiddleware> _logger;
    public ValidationMiddleware(RequestDelegate next, ILogger<ValidationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Exception ocurred: {Message}", exception.Message);

            ExceptionDetails errorInfo = GetExceptionDetails(exception);

            var problemDetails = new ProblemDetails
            {
                Status = errorInfo.Status,
                Type = errorInfo.Type,
                Title = errorInfo.Title,
                Detail = errorInfo.Detail,
            };

            if (errorInfo.Errors is not null)
            {
                problemDetails.Extensions["errors"] = errorInfo.Errors;
            }

            context.Response.StatusCode = errorInfo.Status;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }

    }

    private static ExceptionDetails GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            ValidationException validationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "ValidationFailure", 
                "Validation Error",
                "One or more validation errors has ocurred",
                validationException.Errors),
            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError, 
                "ServerError",
                "Server Error",
                "An unexpected error has ocurred",
                null)
        };
    }

    public sealed record ExceptionDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<ValidationError>? Errors);
}

