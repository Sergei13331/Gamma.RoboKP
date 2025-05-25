using Gamma.RoboKP.Application.Abstractions.Services;
using Gamma.RoboKP.Application.Models.User;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Domain.Enums;
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
    
    public async Task<bool> SetStatus(long id, string status)
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
        
        if (!Enum.TryParse<UserStatus>(status, ignoreCase: true, out var parsedStatus))
        {
            throw new ArgumentException($"Недопустимый статус: {status}", nameof(status));
        }
        
        user.Status = parsedStatus;
        
        var result = await userManager.UpdateAsync(user);
        
        return result.Succeeded;
        
    }
    
    public async Task<string> GetUserStatus(long id)
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
        return user.Status.ToString();
    }

    public async Task<bool> UpdateUser(long id, UserToUpdate userToUpdate)
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
        
        if (userToUpdate.FirstName != null) user.FirstName = userToUpdate.FirstName;
        
        if(userToUpdate.SurName != null) user.Surname = userToUpdate.SurName;
        
        if(userToUpdate.LastName != null) user.LastName = userToUpdate.LastName;
        
        if (userToUpdate.Email != null) user.Email = userToUpdate.Email;
        if(userToUpdate.UserName != null) user.UserName = userToUpdate.UserName;
        
        var result = await userManager.UpdateAsync(user);
        
        return result.Succeeded;
    }

    public async Task<List<UserToGet>> GetAllUsers()
    {
        var usersEntity = await userManager.Users.ToListAsync();

        var usersWithRoles = new List<UserToGet>();

        foreach (var user in usersEntity)
        {
            var roles = await userManager.GetRolesAsync(user);
            usersWithRoles.Add(new UserToGet
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

    public async Task<bool> DeleteUser(long id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user == null) return false;
        
        var result = await userManager.DeleteAsync(user);
        
        return result.Succeeded;
    }

    public async Task<UserToGet> GetUserById(long id)
    {
        var userEntity = await userManager.FindByIdAsync(id.ToString());
        if (userEntity == null)
        {
            throw new EntityNotFoundException(
                new List<IdentityError>{new IdentityError()
                {
                    Description  = $"Пользователь c id {id} не найден",
                    Code = "User not found" } });
        }
        var roles = await userManager.GetRolesAsync(userEntity);
        var user = new UserToGet(

            userEntity.Id,
            userEntity.FirstName,
            userEntity.Surname,
            userEntity.LastName,
            userEntity.Email,
            userEntity.Status,
            roles.FirstOrDefault(),
            userEntity.Company
        );
        return user;
    } 
}
