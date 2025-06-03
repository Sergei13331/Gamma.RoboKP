using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Domain.Abstractions.Repositories;

public interface ITokenRepository
{
    Task SaveToken(RefreshTokenEntity refreshToken);
    Task<RefreshTokenEntity?> GetByHashToken(string tokenHash);
    Task<bool> Delete(string hash);
    Task<bool> DeleteAllUserTokens(long userId);
}