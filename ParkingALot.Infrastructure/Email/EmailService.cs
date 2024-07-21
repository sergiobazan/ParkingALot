using ParkingALot.Application.Abstractions.Email;

namespace ParkingALot.Infrastructure.Email;

internal class EmailService : IEmailService
{
    public Task SendEmailAsync(string recipent, string subject, string content)
    {
        throw new NotImplementedException();
    }
}
