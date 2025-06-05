using Gamma.RoboKP.Domain.Abstractions.Services;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Domain.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Gamma.RoboKP.Application.Services;

public class MailService(IOptions<EmailConfiguration> emailConfiguration) : IMailService
{
    private readonly EmailConfiguration _emailConfiguration = emailConfiguration.Value;

    public bool SendMail(MailData mailData)
    {
            MimeMessage emailMessage = new MimeMessage();
            MailboxAddress emailFrom = new MailboxAddress(_emailConfiguration.Name, _emailConfiguration.EmailId);
            emailMessage.From.Add(emailFrom);
            MailboxAddress emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
            emailMessage.To.Add(emailTo);
            emailMessage.Subject = mailData.EmailSubject;
            BodyBuilder emailBodyBuilder = new BodyBuilder();
            emailBodyBuilder.TextBody = mailData.EmailBody;
            emailMessage.Body = emailBodyBuilder.ToMessageBody();

            SmtpClient mailClient = new SmtpClient();
            mailClient.Connect(_emailConfiguration.Host, _emailConfiguration.Port, MailKit.Security.SecureSocketOptions.SslOnConnect);
            mailClient.Authenticate(_emailConfiguration.EmailId, _emailConfiguration.Password);
            mailClient.Send(emailMessage);
            mailClient.Disconnect(true);
            mailClient.Dispose();
            return true;
        
        
    }
}