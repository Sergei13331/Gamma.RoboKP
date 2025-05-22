using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gamma.RoboKP.Application.Abstractions.Auth;
using Gamma.RoboKP.Application.Abstractions.Repositories;
using Gamma.RoboKP.Application.Abstractions.Services;
using Gamma.RoboKP.Application.Models.Authentication;
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

        await tokenRepository.SaveToken(Guid.NewGuid(), hashed, expiresAt, userId);
        return refreshToken;  
    }
    
    public string GenerateAccessToken(UserResponse userRegisterModel)
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
            // userRegisterModel.Token = handler.WriteToken(token);
            //
            // var refreshToken = refreshTokenService.GenerateRefreshToken();
            // var hashToken = refreshTokenService.HashToken(refreshToken);
            // var refreshTokenExpire = DateTime.UtcNow.AddDays(_authOptions.RefreshTokenExpireDays);
            // userRegisterModel.RefreshToken = refreshToken;
            //
            // tokenRepository.SaveToken(Guid.NewGuid(), hashToken, refreshTokenExpire, userRegisterModel.Id).GetAwaiter()
            //     .GetResult();
            //
            return handler.WriteToken(token);
    }
    
    public ClaimsIdentity GenerateClaims(UserResponse userRegisterModel)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.Name, userRegisterModel.Email));
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userRegisterModel.Id.ToString()));
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Aud, "test"));
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Iss, "test1"));
        claims.AddClaim(new Claim(ClaimTypes.Role, userRegisterModel.Role.ToString()));
    
        return claims;
    }
}