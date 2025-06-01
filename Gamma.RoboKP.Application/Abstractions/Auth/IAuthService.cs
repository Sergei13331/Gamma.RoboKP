using Gamma.RoboKP.Application.Models.Authentication;

namespace Gamma.RoboKP.Application.Abstractions.Auth;

public interface IAuthService
{
    Task<UserResponse> Register(UserRegisterDto userRegisterDto);
    Task<UserResponse> Login(UserLoginDto userLoginDto);
    
    Task<UserResponse?> RefreshAccessToken(string refreshToken);
    
    // обычно интерфесы в domain
    
    // агрегат рут это собирает все сущности
    
    // почитать про евенты 
    
    // unitOfWork паттерн
    // маппить из инфраструктуры в domain
    
    // можно добавить 4 слой для контроллеров потому что по шаблону cqrs контроллеры делять на 2 ввод и еще че то
    
    //
}