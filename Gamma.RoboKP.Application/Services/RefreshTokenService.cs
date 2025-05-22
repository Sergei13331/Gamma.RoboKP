using System.Security.Cryptography;
using System.Text;
using Gamma.RoboKP.Application.Abstractions.Auth;
using Gamma.RoboKP.Application.Abstractions.Repositories;
using Gamma.RoboKP.Application.Abstractions.Services;
using Gamma.RoboKP.Application.Extensions;

namespace Gamma.RoboKP.Application.Services;

public class RefreshTokenService(ITokenRepository tokenRepository) : IRefreshTokenService
{
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    
    public bool VerifyRefreshToken(string refreshTokenFromCookies, string tokenHash)
    {
        var hashTokenFromCookies = HashToken(refreshTokenFromCookies);
        
        return hashTokenFromCookies == tokenHash;
    }
    
    public string HashToken(string token)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(token);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
    
    //
    // public Task SaveRefreshToken(Guid tokenId, string token, DateTime expiresAt, long userId)
    // {
    //     return tokenRepository.SaveToken(tokenId, token, expiresAt, userId);
    // }
}