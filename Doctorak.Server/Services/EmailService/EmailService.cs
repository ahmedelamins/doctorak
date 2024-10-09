namespace Doctorak.Server.Services.EmailService;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }
    public async Task SendEmail(string toEmail, string subject, string body)
    {
        try
        {
            var smptSettings = _config.GetSection("SmptSettings");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
