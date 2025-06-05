namespace Gamma.RoboKP.Domain.Entities;

public class MailData
{
    public MailData()
    {
        
    }
    public MailData(string id, string name, string subject, string body)
    {
        EmailToId = id;
        EmailToName = name;
        EmailSubject = subject;
        EmailBody = body;
    }    
    
    public string? EmailToId { get; set; }
    public string? EmailToName { get; set; }
    public string? EmailSubject { get; set; }
    public string? EmailBody { get; set; }
}