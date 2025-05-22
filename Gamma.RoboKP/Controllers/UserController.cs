using Gamma.RoboKP.Application.Abstractions.Auth;
using Gamma.RoboKP.Application.Models.Authentication;
using Gamma.RoboKP.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gamma.RoboKP.Filters.ExceptionsFilters;
using Microsoft.Extensions.Options;
using AuthOptions = Gamma.RoboKP.Domain.Options.AuthOptions;

namespace Gamma.RoboKP.Controllers;
[ApiController]
[Route("api/user")]
public class UserController(IOptions<AuthOptions> authOptions, IAuthService authService, IUserService userService) : ControllerBase
{
    private readonly AuthOptions _authOptions = authOptions.Value;
    
    [HttpPost("register")]
    [UserExceptions]
    public async Task<ActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
    {
        var result =  await authService.Register(userRegisterDto);
        
        Response.Cookies.Append("access_token", result.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(_authOptions.ExpireMinutes),
        });
        
        Response.Cookies.Append("refresh_token", result.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(_authOptions.RefreshTokenExpireDays),
        });
        
        return Ok(result);
    }
    
    [HttpPost("login")]
    [UserExceptions]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var result = await authService.Login(userLoginDto);

        Response.Cookies.Append("access_token", result.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(_authOptions.ExpireMinutes),
        });
        
        Response.Cookies.Append("refresh_token", result.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(_authOptions.RefreshTokenExpireDays),
        });
        
        return Ok(result);
    }

    [HttpGet("role")]
    [Authorize]
    public async Task<ActionResult<string>> GetUserRole([FromQuery] string email)
    {
        var result = await userService.GetUserRole(email);
        
        if (result is null)
        {
            return NotFound($"Пользователь с почтой {email} не найден");
        }
        return Ok(result);
    }
}
