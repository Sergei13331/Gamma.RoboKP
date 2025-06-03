using System.Security.Claims;
using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Domain.Abstractions.Services;

public interface ITokenService
{
    string GenerateAccessToken(User userRegisterModel);
    Task<string> GenerateRefreshToken(long userId);
    ClaimsIdentity GenerateClaims(User userRegisterModel);
}