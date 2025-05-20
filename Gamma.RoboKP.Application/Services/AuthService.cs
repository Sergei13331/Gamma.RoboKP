using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Gamma.RoboKP.Application.Abstractions.Auth;
using Gamma.RoboKP.Application.Models.Authentication;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Domain.Exceptions;
using Gamma.RoboKP.Domain.Models;
using Gamma.RoboKP.Domain.Options;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Exception = System.Exception;

namespace Gamma.RoboKP.Application.Services;
public class AuthService(IOptions<AuthOptions> authOptions,
    UserManager<UserEntity> userManager) : IAuthService
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
            
            var result = await userManager.AddToRoleAsync(user, RoleConsts.AdminGamma); // изменить на 
            if (result.Succeeded)
            {
                var response = new UserResponse
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    SurName = user.Surname,
                    LastName = user.LastName,
                    Role = RoleConsts.AdminGamma,
                    Status = user.Status,
                    Email = user.Email,
                    Company = user.Company,
                    UserName = user.UserName,
                }; 
                return GenerateToken(response);
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
            throw new EntityNotFoundException($"Пользователь с почтой {userLoginDto.Email} не найден");
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
            return GenerateToken(userResponse);
        }

        throw new AuthenticationException("Неверный пароль");
    }
    
    public UserResponse GenerateToken(UserResponse userRegisterModel)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_authOptions.TokenPrivateKey);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);
        
        var claims = new Dictionary<string, object>
        {
            {ClaimTypes.Name, userRegisterModel.Email!},
            {ClaimTypes.NameIdentifier, userRegisterModel.Id.ToString()},
            {JwtRegisteredClaimNames.Aud, "test"},
            {JwtRegisteredClaimNames.Iss, "test"}
        };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(userRegisterModel),
            Expires = DateTime.UtcNow.AddMinutes(_authOptions.ExpireMinutes),
            SigningCredentials = credentials,
            Claims = claims,
            Audience = "test",
            Issuer = "test"
        };

        var token = handler.CreateToken(tokenDescriptor);
        userRegisterModel.Token = handler.WriteToken(token);

        return userRegisterModel;
    }

    private static ClaimsIdentity GenerateClaims(UserResponse userRegisterModel)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.Name, userRegisterModel.Email));
        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userRegisterModel.Id.ToString()));
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Aud, "test"));
        claims.AddClaim(new Claim(JwtRegisteredClaimNames.Iss, "test"));
        claims.AddClaim(new Claim(ClaimTypes.Role, userRegisterModel.Role.ToString()));

        return claims;
    }
}