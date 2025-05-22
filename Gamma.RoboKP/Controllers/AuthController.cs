using Gamma.RoboKP.Application.Abstractions.Auth;
using Gamma.RoboKP.Application.Models.Authentication;
using Gamma.RoboKP.Filters.ExceptionsFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AuthOptions = Gamma.RoboKP.Domain.Options.AuthOptions;

namespace Gamma.RoboKP.Controllers;
[ApiController]
[Route("api/auth")]
public class AuthController(IOptions<AuthOptions> authOptions,
    IAuthService authService) : ControllerBase
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
    
    [HttpPost("refresh")]
    [Authorize]
    public async Task<ActionResult<UserResponse>> RefreshToken()
    {
        var refreshToken = Request.Cookies["refresh_token"];
        if (string.IsNullOrEmpty(refreshToken)) return Unauthorized();
        
        var result = await authService.RefreshAccessToken(refreshToken);
        if (result is null)
        {
            return NotFound();
        }
        Response.Cookies.Append("access_token", result.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(_authOptions.ExpireMinutes),
        });
        return Ok(result);
    }
}