namespace Gamma.RoboKP.Domain.Entities;

public class RefreshTokenEntity : BaseEntity<Guid>
{
    private RefreshTokenEntity(string tokenHash, DateTime expiresAt, long userId)
    {
        TokenHash = tokenHash;
        ExpiresAt = expiresAt;
        UserId = userId;
    }
    
    public string TokenHash { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public long UserId { get; private set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    
    public static RefreshTokenEntity Create(string tokenHash, DateTime expiresAt, long userId)
    {
        var entity = new RefreshTokenEntity(tokenHash, expiresAt, userId)
        {
            Id = Guid.NewGuid(),
        };
        return entity;
    }

    public bool Validate()
    {
        return !IsExpired;
    }
    
}