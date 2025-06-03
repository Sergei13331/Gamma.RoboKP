using Gamma.RoboKP.Domain.Abstractions.Repositories;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Gamma.RoboKP.Infrastructure.Repositories;

public class TokenRepository(RoboKpDbContext context) : ITokenRepository
{
    public async Task SaveToken(RefreshTokenEntity refreshToken)
    {
        
        await context.RefreshTokens.AddAsync(refreshToken);
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