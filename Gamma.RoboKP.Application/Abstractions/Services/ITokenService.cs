using System.Security.Claims;
using Gamma.RoboKP.Application.Models.Authentication;

namespace Gamma.RoboKP.Application.Abstractions.Services;

public interface ITokenService
{
    UserResponse GenerateToken(UserResponse userRegisterModel);
    ClaimsIdentity GenerateClaims(UserResponse userRegisterModel);
}