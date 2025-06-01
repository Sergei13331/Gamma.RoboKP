namespace Gamma.RoboKP.Domain.Entities;

public class RefreshTokenEntity
{
    public Guid Id { get; set; }
    public string TokenHash { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public long UserId { get; set; }
    
    
    //TODO: проверки на исчечение срока действия
}