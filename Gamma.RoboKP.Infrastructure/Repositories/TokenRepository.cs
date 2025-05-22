using Gamma.RoboKP.Application.Abstractions.Repositories;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;


namespace Gamma.RoboKP.Infrastructure.Repositories;

public class TokenRepository(RoboKpDbContext context) : ITokenRepository
{
    public async Task SaveToken(Guid tokenId, string token, DateTime expiresAt , long userId)
    {
        var newEntity = new RefreshTokenEntity
        {
            Id = tokenId,
            TokenHash = token,
            ExpiresAt = expiresAt,
            CreatedAt = DateTime.UtcNow,
            UserId = userId,
        };
        
        await context.RefreshTokens.AddAsync(newEntity);
        await context.SaveChangesAsync();
    }

    public async Task<RefreshTokenEntity?> GetByHashToken(string tokenHash)
    {
        return await context.RefreshTokens.FirstOrDefaultAsync(t => t.TokenHash == tokenHash);
    }

    public async Task<bool> Delete(string hash)
    {
        var tokenEntity = await context.RefreshTokens.FirstOrDefaultAsync(t => t.TokenHash == hash);
        
        if (tokenEntity == null) return false;
        
        context.RefreshTokens.Remove(tokenEntity);
        await context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteAllUserTokens(long userId)
    {
        var tokenEntities = await context.RefreshTokens.Where(t => t.UserId == userId).ExecuteDeleteAsync();
        return tokenEntities > 0;
    }
}