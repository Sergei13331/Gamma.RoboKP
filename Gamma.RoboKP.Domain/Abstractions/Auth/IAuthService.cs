using Gamma.RoboKP.Domain.Entities;

namespace Gamma.RoboKP.Domain.Abstractions.Auth;

public interface IAuthService
{
    Task<User> Register(User userRegister, string password);
    Task<User> Login(string email, string password);
    
    Task<User?> RefreshAccessToken(string refreshToken);
    
    // обычно интерфесы в domain
    
    // агрегат рут это собирает все сущности
    
    // почитать про евенты 
    
    // unitOfWork паттерн
    // маппить из инфраструктуры в domain
    
    // можно добавить 4 слой для контроллеров потому что по шаблону cqrs контроллеры делять на 2 ввод и еще че то
    
    //
}