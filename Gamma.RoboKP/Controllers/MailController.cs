using Gamma.RoboKP.Domain.Abstractions.Services;
using Gamma.RoboKP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Gamma.RoboKP.Controllers;

[ApiController]
[Route("api/email")]
public class MailController(IMailService mailService) : ControllerBase
{
    [HttpPost]
    public bool Send(MailData mailData)
    {
        return mailService.SendMail(mailData);
    }
}