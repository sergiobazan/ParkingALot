using Microsoft.Extensions.Logging;
using ParkingALot.Application.Abstractions.Email;

namespace ParkingALot.Infrastructure.Email;

internal class EmailService(ILogger<EmailService> logger) : IEmailService
{
    public Task SendEmailAsync(string recipent, string subject, string content)
    {
        logger.LogInformation("Welcome Driver {Email} - {Subject} - {Content}", recipent, subject, content);

        return Task.CompletedTask;
    }
}
