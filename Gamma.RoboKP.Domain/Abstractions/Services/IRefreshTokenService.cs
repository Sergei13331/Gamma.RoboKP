namespace Gamma.RoboKP.Domain.Abstractions.Services;

public interface IRefreshTokenService
{
    string GenerateRefreshToken();
    bool VerifyRefreshToken(string refreshTokenFromCookies, string tokenHash);
    string HashToken(string refreshToken);
    Task<bool> DeleteRefreshToken(string refreshToken);
    Task<bool> DeleteAllUserRefreshTokens(long userId);
}