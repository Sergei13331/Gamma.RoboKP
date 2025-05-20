namespace Gamma.RoboKP.Application.Abstractions.Auth;

public interface IUserService
{
    Task<string?> GetUserRole(string email);
}