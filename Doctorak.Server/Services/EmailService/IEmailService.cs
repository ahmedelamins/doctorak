﻿namespace Doctorak.Server.Services.EmailService;

public interface IEmailService
{
    Task SendEmail(string toEmail, string subject, string body);
}
