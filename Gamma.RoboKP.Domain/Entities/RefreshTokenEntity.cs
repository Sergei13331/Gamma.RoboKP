namespace Gamma.RoboKP.Domain.Entities;

public class RefreshTokenEntity
{
    public long Id { get; set; }
    public string IpAddress { get; set; } = "";
    public string UserId { get; set; } = "";
    public string TokenHash { get; set; } = "";
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    public string Subject { get; set; } = "";
}