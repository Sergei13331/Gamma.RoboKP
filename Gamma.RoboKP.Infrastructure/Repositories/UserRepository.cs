using Gamma.RoboKP.Application.Abstractions.Repositories;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Domain.ValueObject;
using Gamma.RoboKP.Infrastructure.Identity;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;

namespace Gamma.RoboKP.Infrastructure.Repositories;

public class UserRepository(UserManager<AppUser> userManager, IMapper mapper) : IUserRepository
{
    public async Task<User?> FindByEmailAsync(string email)
    {
        var appUser = await userManager.FindByEmailAsync(email);
        
        if (appUser == null) return null;
        
        var entity = mapper.Map<AppUser, User>(appUser);
        
        return entity;
    }

    public async Task<bool> AddAsync(User user, string password)
    {
        var appUser = mapper.Map<User, AppUser>(user);
        appUser.UserName = user.Email;
        var result = await userManager.CreateAsync(appUser, password);
        
        return result.Succeeded;
    }

    public async Task<IdentityResult> AddToRole(User user, string role)
    {
        var appUser = await userManager.FindByIdAsync(user.Id.ToString());

        if (appUser == null)
            throw new InvalidOperationException($"User with ID {user.Id} not found");

        var result = await userManager.AddToRoleAsync(appUser, role);
        return result;
    }

    public async Task<bool> CheckPassword(User user, string password)
    {
        var userApp = await userManager.FindByIdAsync(user.Id.ToString());
        if (userApp == null) return false;
        
        var result = await userManager.CheckPasswordAsync(userApp, password);
        
        return result;
    }

    public async Task<string?> GetRole(User user)
    {
        var userApp = await userManager.FindByIdAsync(user.Id.ToString());
        
        if (userApp == null) return null;
        
        var role = await userManager.GetRolesAsync(userApp);
        return role.FirstOrDefault();
    }
}