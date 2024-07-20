namespace ParkingALot.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendEmailAsync(string recipent, string subject, string content);
}
