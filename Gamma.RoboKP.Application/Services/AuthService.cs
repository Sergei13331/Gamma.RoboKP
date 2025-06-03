using Gamma.RoboKP.Domain.Abstractions.Auth;
using Gamma.RoboKP.Domain.Abstractions.Repositories;
using Gamma.RoboKP.Domain.Abstractions.Services;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Domain.Exceptions;
using Gamma.RoboKP.Domain.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Exception = System.Exception;

namespace Gamma.RoboKP.Application.Services;
public class AuthService(ITokenRepository refreshTokenRepository,
    IRefreshTokenService refreshTokenService,
    ITokenService tokenService,
    IUserRepository userRepository,
    IMapper mapper) : IAuthService
{
    public async Task<User> Register(User userRegister, string password)
    {
        var existingUser = await userRepository.FindByEmailAsync(userRegister.Email);
        
        if (existingUser != null) 
        {
            throw new NotValidUserException(
                userRegister,
                new List<IdentityError>() { new IdentityError() {
                    Description="Пользователь с такой почтой уже существует",
                    Code="Email Duplicate"} });
        }
        
        var createUserResult = await userRepository.AddAsync(userRegister, password);
        if (createUserResult)
        {
            var user = await userRepository.FindByEmailAsync(userRegister.Email);

            if (user == null)
            {
                throw new Exception("Что то пошло не так..."); // пока хз
            }
            
            var result = await userRepository.AddToRole(user, RoleConsts.ManagerPartner);
            if (result.Succeeded)
            {
                return user;
            }
    
            throw new Exception($"Errors: {string.Join(";", result.Errors
                .Select(x => $"{x.Code} {x.Description}"))}");
        }
        throw new Exception($"Регистрация не удалась: ");
    }
    
    
    public async Task<User> Login(string email, string password)
    {
        var user = await userRepository.FindByEmailAsync(email);
        
        if (user == null)
        {
            throw new EntityNotFoundException(
                new List<IdentityError>{new IdentityError()
                {
                  Description  = $"Пользователь с почтой {email} не найден",
                  Code = "Email not found" } });
        }
        
        var checkPasswordResult = await userRepository.CheckPassword(user, password);
        
        if (checkPasswordResult)
        {
            var userRole = await userRepository.GetRole(user);

            if (userRole == null)
            {
                throw new EntityNotFoundException(
                    new List<IdentityError>{new IdentityError()
                    {
                        Description  = "Ошибка. Пользователю не присвоена роль",
                        Code = "Exception. User role not found." } });
            }
            
            return user;
        }
    
        throw new PasswordFailedException(
            new List<IdentityError>{new IdentityError()
            {
                Description = "Неверный пароль",
                Code = "Invalid Password"} });
    }
    
    public async Task<User?> RefreshAccessToken(string refreshToken)
    {
        var hashToken = refreshTokenService.HashToken(refreshToken);
        
        var refreshTokenEntity = await refreshTokenRepository.GetByHashToken(hashToken);
    
        if (refreshTokenEntity == null || refreshTokenEntity.ExpiresAt < DateTime.Now)
        {
            return null;
        }
        
        var user = await userRepository.FindByIdAsync(refreshTokenEntity.UserId);
        if (user == null)
        {
            return null;
        }
        
        var userRole = await userRepository.GetRole(user);

        if (userRole == null)
        {
            throw new EntityNotFoundException(
                new List<IdentityError>{new IdentityError()
                {
                    Description  = "Ошибка. Пользователю не присвоена роль",
                    Code = "Exception. User role not found." } });
        }
        
        return user;
    }
}