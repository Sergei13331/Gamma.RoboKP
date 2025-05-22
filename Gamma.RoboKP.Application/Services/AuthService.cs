using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gamma.RoboKP.Application.Abstractions.Auth;
using Gamma.RoboKP.Application.Abstractions.Repositories;
using Gamma.RoboKP.Application.Abstractions.Services;
using Gamma.RoboKP.Application.Extensions;
using Gamma.RoboKP.Application.Models.Authentication;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Domain.Exceptions;
using Gamma.RoboKP.Domain.Models;
using Gamma.RoboKP.Domain.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Exception = System.Exception;

namespace Gamma.RoboKP.Application.Services;
public class AuthService(IOptions<AuthOptions> authOptions,
    UserManager<UserEntity> userManager,
    ITokenRepository refreshTokenRepository,
    IRefreshTokenService refreshTokenService,
    ITokenService tokenService) : IAuthService
{
    private readonly AuthOptions _authOptions = authOptions.Value;
    
    public async Task<UserResponse> Register(UserRegisterDto userRegisterDto)
    {
        var entity = new UserEntity
        {
            FirstName = userRegisterDto.Name,
            Surname = userRegisterDto.Surname,
            LastName = userRegisterDto.LastName,
            Status = userRegisterDto.Status,
            Email = userRegisterDto.Email,
            Company = userRegisterDto.Company,
            UserName = userRegisterDto.Email,
        };
        
        var existingUser = await userManager.FindByEmailAsync(entity.Email);
        
        if (existingUser != null) 
        {
            throw new NotValidUserException(
                entity,
                new List<IdentityError>() { new IdentityError() {
                    Description="Пользователь с такой почтой уже существует",
                    Code="Email Duplicate"} });
        }
        
        var createUserResult = await userManager.CreateAsync(entity, userRegisterDto.Password);

        if (createUserResult.Succeeded)
        {
            var user = await userManager.FindByEmailAsync(userRegisterDto.Email);
            
            var result = await userManager.AddToRoleAsync(user, RoleConsts.ManagerPartner); 
            if (result.Succeeded)
            {
                var response = new UserResponse
                {      
                    Id = user.Id,
                    FirstName = user.FirstName,
                    SurName = user.Surname,
                    LastName = user.LastName,
                    Role = RoleConsts.ManagerPartner,
                    Status = user.Status,
                    Email = user.Email,
                    Company = user.Company,
                    UserName = user.UserName,
                }; 
                
                response.Token = tokenService.GenerateAccessToken(response);
                response.RefreshToken = await tokenService.GenerateRefreshToken(user.Id);
                
                return response;
            }

            throw new Exception($"Errors: {string.Join(";", result.Errors
                .Select(x => $"{x.Code} {x.Description}"))}");
        }
        throw new Exception($"Регистрация не удалась: {string.Join(" | ", createUserResult.Errors.Select(e => $"{e.Code}: {e.Description}"))}");
    }
    

    public async Task<UserResponse> Login(UserLoginDto userLoginDto)
    {
        var user = await userManager.FindByEmailAsync(userLoginDto.Email);
        
        if (user == null)
        {
            throw new EntityNotFoundException(
                new List<IdentityError>{new IdentityError()
                {
                  Description  = $"Пользователь с почтой {userLoginDto.Email} не найден",
                  Code = "Email not found" } });
        }
        var checkPasswordResult = await userManager.CheckPasswordAsync(user, userLoginDto.Password);
        
        if (checkPasswordResult)
        {
            var userRole = await userManager.GetRolesAsync(user);

            var userResponse = new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                SurName = user.Surname,
                LastName = user.LastName,
                Role = userRole.FirstOrDefault()!,
                Status = user.Status,
                Email = user.Email,
                Company = user.Company,
                UserName = user.UserName,
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
        
        var user = await userManager.FindByIdAsync(refreshTokenEntity.UserId.ToString());
        if (user == null)
        {
            return null;
        }
        
        var userRole = await userManager.GetRolesAsync(user);

        var userResponse = new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            SurName = user.Surname,
            LastName = user.LastName,
            Role = userRole.FirstOrDefault()!,
            Status = user.Status,
            Email = user.Email,
            Company = user.Company,
            UserName = user.UserName,
        };
        userResponse.Token = tokenService.GenerateAccessToken(userResponse);
        return userResponse;
    }

    public Task<UserResponse> LogOut()
    {
        throw new NotImplementedException();
    }
}