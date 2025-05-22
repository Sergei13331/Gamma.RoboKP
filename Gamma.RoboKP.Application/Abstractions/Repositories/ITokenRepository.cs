using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Application.Abstractions.Repositories;

public interface ITokenRepository
{
    Task SaveToken(Guid tokenId, string token, DateTime expiresAt , long userId);
    Task<RefreshTokenEntity?> GetByHashToken(string tokenHash);
    Task<bool> Delete(string hash);
    Task<bool> DeleteAllUserTokens(long userId);
}