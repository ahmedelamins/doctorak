using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace Doctorak.Server.Services.EmailService;

public class EmailService : IEmailService
{
    private readonly TransactionalEmailsApi _transactionalEmailsApi;

    public EmailService(string apiKey)
    {
        Configuration.Default.ApiKey.Add("api-key", apiKey);
        _transactionalEmailsApi = new TransactionalEmailsApi();
    }


    async System.Threading.Tasks.Task IEmailService.SendEmail(string to, string firstName, string subject, string body)
    {
        var sender = new SendSmtpEmailSender("Doctorak", "no-reply@doctorak.com");
        var recipients = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(to, firstName) };

        var sendSmtpEmail = new SendSmtpEmail(
            sender: sender,
            to: recipients,
            htmlContent: body,
            subject: subject
        );

        try
        {
            // Send the email using Brevo's Transactional API
            await _transactionalEmailsApi.SendTransacEmailAsync(sendSmtpEmail);
        }
        catch (ApiException ex)
        {
            // Handle any API exceptions
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }
}
