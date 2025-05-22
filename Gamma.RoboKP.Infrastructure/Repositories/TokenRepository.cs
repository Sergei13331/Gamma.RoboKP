using Gamma.RoboKP.Application.Abstractions.Repositories;
using Gamma.RoboKP.Application.Extensions;
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

    public async Task<RefreshTokenEntity?> GetByUserId(long tokenId)
    {
        return await context.RefreshTokens
            .Where(t => t.UserId == tokenId)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();
    }
}