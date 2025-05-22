using Gamma.RoboKP.Application.Abstractions.Services;
using Gamma.RoboKP.Application.Models.User;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Domain.Exceptions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gamma.RoboKP.Application.Services;

public class UserService(UserManager<UserEntity> userManager) : IUserService
{
    public async Task<string?> GetUserRole(long id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            return null;
        }
        
        var role = await userManager.GetRolesAsync(user);
        
        return role.FirstOrDefault()!;
    }

    public async Task SetUserRole(long id, string role)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            throw new EntityNotFoundException(
                new List<IdentityError>{new IdentityError()
                {
                    Description  = $"Пользователь с id {id} не найден",
                    Code = "User not found" } });
        }
        var currentUserRoles = await userManager.GetRolesAsync(user);
        await userManager.RemoveFromRolesAsync(user, currentUserRoles);
        
        var isSuccessful = await userManager.AddToRoleAsync(user, role);
        if (!isSuccessful.Succeeded)
        {
            var errors = string.Join("; ", isSuccessful.Errors.Select(e => $"{e.Code}: {e.Description}"));
            throw new Exception($"Ошибка при добавлении роли: {errors}");
        }
    }

    public async Task<List<UserToGetAll>> GetAllUsers()
    {
        var usersEntity = await userManager.Users.ToListAsync();

        var usersWithRoles = new List<UserToGetAll>();

        foreach (var user in usersEntity)
        {
            var roles = await userManager.GetRolesAsync(user);
            usersWithRoles.Add(new UserToGetAll
            (
                user.Id,
                user.FirstName,
                user.Surname,
                user.LastName,
                user.Email,
                user.Status,
                roles.FirstOrDefault(),
                user.Company
            ));
        }

        return usersWithRoles;
    }
}
