using Gamma.RoboKP.Application.Models.Authentication;

namespace Gamma.RoboKP.Application.Abstractions.Auth;

public interface IAuthService
{
    Task<UserResponse> Register(UserRegisterDto userRegisterDto);
    Task<UserResponse> Login(UserLoginDto userLoginDto);
    Task<UserResponse> LogOut();
    
}