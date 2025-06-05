using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Domain.Abstractions.Services;

public interface IMailService
{
    bool SendMail(MailData mailData);
}