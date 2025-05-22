using Gamma.RoboKP.Application.Models.User;

namespace Gamma.RoboKP.Application.Abstractions.Services;

public interface IUserService
{
    Task<string?> GetUserRole(long id);
    Task SetUserRole(long id, string role);
    Task<List<UserToGetAll>> GetAllUsers();
}