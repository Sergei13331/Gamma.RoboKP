using Gamma.RoboKP.Application.Extensions;
using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Application.Abstractions.Repositories;

public interface ITokenRepository
{
    Task SaveToken(Guid tokenId, string token, DateTime expiresAt , long userId);
    Task<RefreshTokenEntity?> GetByUserId(long tokenId);
}