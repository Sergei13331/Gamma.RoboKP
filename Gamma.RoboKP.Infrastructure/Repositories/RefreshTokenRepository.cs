using Gamma.RoboKP.Application.Abstractions.Repositories;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Infrastructure.Context;

namespace Gamma.RoboKP.Infrastructure.Repositories;

public class RefreshTokenRepository(RoboKpDbContext context) : IRefreshTokeRepository
{
    public async Task<RefreshTokenEntity> Add(RefreshTokenEntity refreshToken)
    {
        var entity = await context.RefreshToken.AddAsync(refreshToken);
        await context.SaveChangesAsync();
        return entity.Entity;
    }
}