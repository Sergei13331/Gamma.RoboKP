using Gamma.RoboKP.Application.Abstractions.Auth;
using Gamma.RoboKP.Application.Abstractions.Repositories;
using Gamma.RoboKP.Application.Abstractions.Services;
using Gamma.RoboKP.Application.Models.Authentication;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Domain.Enums;
using Gamma.RoboKP.Domain.Exceptions;
using Gamma.RoboKP.Domain.Models;
using Gamma.RoboKP.Domain.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Exception = System.Exception;

namespace Gamma.RoboKP.Application.Services;
public class AuthService(IOptions<AuthOptions> authOptions,
    ITokenRepository refreshTokenRepository,
    IRefreshTokenService refreshTokenService,
   // UserManager<User> userManager,
    ITokenService tokenService,
    IUserRepository userRepository) : IAuthService
{
    private readonly AuthOptions _authOptions = authOptions.Value;
    
    public async Task<UserResponse> Register(UserRegisterDto userRegisterDto)
    {
        var entity = User.Create(userRegisterDto.FirstName, userRegisterDto.SurName, userRegisterDto.LastName,
            UserStatus.Silver, UserRole.ManagerPartner, userRegisterDto.Company, userRegisterDto.Email);
        
        var existingUser = await userRepository.FindByEmailAsync(entity.Email);
        
        if (existingUser != null) 
        {
            throw new NotValidUserException(
                entity,
                new List<IdentityError>() { new IdentityError() {
                    Description="Пользователь с такой почтой уже существует",
                    Code="Email Duplicate"} });
        }
        
        var createUserResult = await userRepository.AddAsync(entity, userRegisterDto.Password);
        if (createUserResult)
        {
            var user = await userRepository.FindByEmailAsync(userRegisterDto.Email);

            if (user == null)
            {
                throw new Exception("Что то пошло не так...");
            }
            
            var result = await userRepository.AddToRole(user, RoleConsts.ManagerPartner);
            if (result.Succeeded)
            {
                var response = new UserResponse
                {      
                    Id = user.Id,
                    FirstName = user.FirstName,
                    SurName = user.SurName,
                    LastName = user.LastName,
                    Role = RoleConsts.ManagerPartner,
                    Status = user.Status.ToString(),
                    Email = user.Email,
                    Company = user.Company,
                    UserName = user.Email,
                }; 
                
                response.Token = tokenService.GenerateAccessToken(response);
                response.RefreshToken = await tokenService.GenerateRefreshToken(user.Id);
                
                return response;
            }
    
            throw new Exception($"Errors: {string.Join(";", result.Errors
                .Select(x => $"{x.Code} {x.Description}"))}");
        }
        throw new Exception($"Регистрация не удалась: ");
    }
    
    
    public async Task<UserResponse> Login(UserLoginDto userLoginDto)
    {
        var user = await userRepository.FindByEmailAsync(userLoginDto.Email);
        
        if (user == null)
        {
            throw new EntityNotFoundException(
                new List<IdentityError>{new IdentityError()
                {
                  Description  = $"Пользователь с почтой {userLoginDto.Email} не найден",
                  Code = "Email not found" } });
        }
        var checkPasswordResult = await userRepository.CheckPassword(user, userLoginDto.Password);
        
        if (checkPasswordResult)
        {
            var userRole = await userRepository.GetRole(user);

            if (userRole == null)
            {
                throw new Exception("Проблема с ролями"); //пока так
            }
            
            var userResponse = new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                SurName = user.SurName,
                LastName = user.LastName,
                Role = userRole,
                Status = user.Status.ToString(),
                Email = user.Email,
                Company = user.Company,
                UserName = user.Email,
            };
            userResponse.Token = tokenService.GenerateAccessToken(userResponse);
            userResponse.RefreshToken = await tokenService.GenerateRefreshToken(user.Id);
                
            return userResponse;
        }
    
        throw new PasswordFailedException(
            new List<IdentityError>{new IdentityError()
            {
                Description = "Неверный пароль",
                Code = "Invalid Password"} });
    }
    
    public async Task<UserResponse?> RefreshAccessToken(string refreshToken)
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
            throw new Exception("Проблема с ролями");
        }
        
        var userResponse = new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            SurName = user.SurName,
            LastName = user.LastName,
            Role = userRole,
            Status = user.Status.ToString(),
            Email = user.Email,
            Company = user.Company,
            UserName = user.Email,
        };
        userResponse.Token = tokenService.GenerateAccessToken(userResponse);
        return userResponse;
    }
}