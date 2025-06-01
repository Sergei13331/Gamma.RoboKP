using Gamma.RoboKP.Application.Abstractions.Repositories;
using Gamma.RoboKP.Application.Abstractions.Services;
using Gamma.RoboKP.Application.Models.User;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Domain.Enums;
using Gamma.RoboKP.Domain.Exceptions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gamma.RoboKP.Application.Services;

public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    public async Task<string?> GetUserRole(long id)
    {
        var user = await userRepository.FindByIdAsync(id);
        if (user == null)
        {
            return null;
        }
        
        var role = await userRepository.GetRole(user);
        
        return role;
    }
    
    public async Task SetUserRole(long id, string role)
    {
        var user = await userRepository.FindByIdAsync(id);
        if (user == null)
        {
            throw new EntityNotFoundException(
                new List<IdentityError>{new IdentityError()
                {
                    Description  = $"Пользователь с id {id} не найден",
                    Code = "User not found" } });
        }
        var currentUserRoles = await userRepository.GetRole(user);
        
        if (currentUserRoles == null)
        {
            throw new Exception("Проблема с ролями");
        }
        await userRepository.RemoveFromRole(user, currentUserRoles);
        
        var isSuccessful = await userRepository.AddToRole(user, role);
        if (!isSuccessful.Succeeded)
        {
            var errors = string.Join("; ", isSuccessful.Errors.Select(e => $"{e.Code}: {e.Description}"));
            throw new Exception($"Ошибка при добавлении роли: {errors}");
        }
    }
    
    public async Task<bool> SetStatus(long id, string status)
    {
        var user = await userRepository.FindByIdAsync(id);
    
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
        
        user.SetStatus(parsedStatus);
        
        var result = await userRepository.UpdateAsync(user);
        
        return result.Succeeded;
    }
    
    public async Task<string> GetUserStatus(long id)
    {
        var user = await userRepository.FindByIdAsync(id);
        
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
        var user = await userRepository.FindByIdAsync(id);
        if (user == null) 
        {
            throw new EntityNotFoundException(
                new List<IdentityError>{new IdentityError()
                {
                    Description  = $"Пользователь с id {id} не найден",
                    Code = "User not found" } });
        }
        
        if (userToUpdate.FirstName != null) user.SetFirstName(userToUpdate.FirstName);
        
        if(userToUpdate.SurName != null) user.SetSurName(userToUpdate.SurName);
        
        if(userToUpdate.LastName != null) user.SetLastName(userToUpdate.LastName);
        
        if (userToUpdate.Email != null) user.SetEmail(userToUpdate.Email);
        
        var result = await userRepository.UpdateAsync(user); // вот тут может быть проблема
        
        return result.Succeeded;
    }
    
    public async Task<List<UserToGet>> GetAllUsers()
    {
        var usersEntity = await userRepository.GetAll();
    
        var usersWithRoles = new List<UserToGet>();
    
        foreach (var user in usersEntity)
        {
            var roles = await userRepository.GetRole(user);
            if (roles == null)
            {
                throw new Exception("Роль не найдкна"); // пока так
            }
            usersWithRoles.Add(new UserToGet
            (
                user.Id,
                user.FirstName,
                user.SurName,
                user.LastName,
                user.Email,
                user.Status,
                roles,
                user.Company
            ));
        }
    
        return usersWithRoles;
    }
    
    public async Task<bool> DeleteUser(long id)
    {
        var user = await userRepository.FindByIdAsync(id);
        if (user == null) return false;
        
        var result = await userRepository.Delete(user);
        
        return result.Succeeded;
    }
    
    public async Task<UserToGet> GetUserById(long id)
    {
        var userEntity = await userRepository.FindByIdAsync(id);
        if (userEntity == null)
        {
            throw new EntityNotFoundException(
                new List<IdentityError>{new IdentityError()
                {
                    Description  = $"Пользователь c id {id} не найден",
                    Code = "User not found" } });
        }
        var role = await userRepository.GetRole(userEntity);
        if (role == null)
        {
            throw new Exception("Проблема с ролями");
        }
        var user = new UserToGet(
    
            userEntity.Id,
            userEntity.FirstName,
            userEntity.SurName,
            userEntity.LastName,
            userEntity.Email,
            userEntity.Status,
            role,
            userEntity.Company
        );
        return user;
    } 
    public async Task<UserToGet?> GetUserByEmail(string email)
    {
        var entity = await userRepository.FindByEmailAsync(email);
        if (entity == null) return null;
        
        var user = mapper.Map<User, UserToGet>(entity);
        
        return user;
    }
}
