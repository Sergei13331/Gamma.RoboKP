using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gamma.RoboKP.Domain.Abstractions.Repositories;
using Gamma.RoboKP.Domain.Abstractions.Services;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Domain.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Gamma.RoboKP.Application.Services;

public class TokenService(IRefreshTokenService refreshTokenService,
    ITokenRepository tokenRepository,
    IOptions<AuthOptions> authOptions) : ITokenService
{
    private readonly AuthOptions _authOptions = authOptions.Value;
    
    public async Task<string> GenerateRefreshToken(long userId)
    {
        var refreshToken = refreshTokenService.GenerateRefreshToken();
        var hashed = refreshTokenService.HashToken(refreshToken);
        var expiresAt = DateTime.UtcNow.AddDays(_authOptions.RefreshTokenExpireDays);
        
        var newToken = RefreshTokenEntity.Create(hashed, expiresAt, userId);
        await tokenRepository.SaveToken(newToken);
        
        return refreshToken;  
    }
    
    public string GenerateAccessToken(User userRegisterModel)
    {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authOptions.TokenPrivateKey);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var claims = new Dictionary<string, object>
            {
                { ClaimTypes.Name, userRegisterModel.Email! },
                { ClaimTypes.NameIdentifier, userRegisterModel.Id.ToString() },
                { JwtRegisteredClaimNames.Aud, "test" },
                { JwtRegisteredClaimNames.Iss, "test1" }
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(userRegisterModel),
                Expires = DateTime.UtcNow.AddMinutes(_authOptions.ExpireMinutes),
                SigningCredentials = credentials,
                Claims = claims,
                Audience = "test",
                Issuer = "test"
            };

            var token = handler.CreateToken(tokenDescriptor);
        
            return handler.WriteToken(token);
    }
    
    public ClaimsIdentity GenerateClaims(User userRegisterModel)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.Name, userRegisterModel.Email));
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userRegisterModel.Id.ToString()));
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Aud, "test"));
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Iss, "test1"));
        claims.AddClaim(new Claim(ClaimTypes.Role, userRegisterModel.Role.ToString()));
        claims.AddClaim(new Claim(ClaimTypes.UserData, userRegisterModel.Status.ToString()));
    
        return claims;
    }
}