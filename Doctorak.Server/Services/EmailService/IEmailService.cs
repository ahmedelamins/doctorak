namespace Doctorak.Server.Services.EmailService;

public interface IEmailService
{
    Task SendEmail(string to, string firstName, string subject, string body);
}
