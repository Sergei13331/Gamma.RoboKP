using System.Security.Claims;
using Gamma.RoboKP.Application.Models.Authentication;

namespace Gamma.RoboKP.Application.Abstractions.Services;

public interface ITokenService
{
    string GenerateAccessToken(UserResponse userRegisterModel);
    Task<string> GenerateRefreshToken(long userId);
    ClaimsIdentity GenerateClaims(UserResponse userRegisterModel);
}