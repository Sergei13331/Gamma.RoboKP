using Gamma.RoboKP.Application.Models.User;

namespace Gamma.RoboKP.Application.Abstractions.Services;

public interface IUserService
{
    Task<string?> GetUserRole(long id);
    Task SetUserRole(long id, string role);
    Task<List<UserToGetAll>> GetAllUsers();
    Task<bool> SetStatus(long id, string status);
    Task<string> GetUserStatus(long id);
    Task UpdateUser(long id, UserToUpdate userToUpdate);
}