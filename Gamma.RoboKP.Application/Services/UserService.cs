using Gamma.RoboKP.Application.Abstractions.Auth;
using Gamma.RoboKP.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Gamma.RoboKP.Application.Services;

public class UserService(UserManager<UserEntity> userManager) : IUserService
{
    public async Task<string?> GetUserRole(string email) // по Id
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return null;
        }
        
        var role = await userManager.GetRolesAsync(user);
        
        return role.FirstOrDefault()!;
    }
}
