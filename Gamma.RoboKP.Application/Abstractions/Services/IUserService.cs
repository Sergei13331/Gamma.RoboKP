using Gamma.RoboKP.Application.Models.User;

namespace Gamma.RoboKP.Application.Abstractions.Services;

public interface IUserService
{
    Task<UserToGet?> GetUserByEmail(string email);
    Task<string?> GetUserRole(long id);
    Task SetUserRole(long id, string role);
    Task<List<UserToGet>> GetAllUsers();
    Task<bool> SetStatus(long id, string status);
    Task<string> GetUserStatus(long id);
    Task<bool> UpdateUser(long id, UserToUpdate userToUpdate);
    Task<bool> DeleteUser(long id);
    Task<UserToGet> GetUserById(long id);
}