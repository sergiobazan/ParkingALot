using MediatR;
using Microsoft.Extensions.Logging;
using ParkingALot.Application.Abstractions.Messaging;

namespace ParkingALot.Application.Abstractions.Behaviors;
public sealed class LoggingBehavior<TRequest, TResult>
    : IPipelineBehavior<TRequest, TResult>
    where TRequest : ICommandBase
{
    private readonly ILogger<LoggingBehavior<TRequest, TResult>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResult>> logger)
    {
        _logger = logger;
    }

    public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;

        try
        {
            _logger.LogInformation("Proccesing Request {Request}", name);

            var result = await next();

            _logger.LogInformation("Request {Request} process successfully", name);

            return result;
        }
        catch (Exception)
        {
            _logger.LogInformation("Fail processing Request {Request}", name);

            throw;
        }
    }
}
