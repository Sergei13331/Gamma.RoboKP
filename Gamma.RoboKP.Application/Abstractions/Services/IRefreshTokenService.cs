namespace Gamma.RoboKP.Application.Abstractions.Services;

public interface IRefreshTokenService
{
    string GenerateRefreshToken();
    bool VerifyRefreshToken(string refreshTokenFromCookies, string tokenHash);
    string HashToken(string refreshToken);
    
    //Task SaveRefreshToken(Guid tokenId, string token, DateTime expiresAt, long userId);
}