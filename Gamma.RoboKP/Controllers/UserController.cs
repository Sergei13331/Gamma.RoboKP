using Gamma.RoboKP.Application.Abstractions.Auth;
using Gamma.RoboKP.Application.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gamma.RoboKP.Filters.ExceptionsFilters;

namespace Gamma.RoboKP.Controllers;
[ApiController]
[Route("api/user")]
public class UserController(IAuthService authService, IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    [UserExceptions]
    public async Task<ActionResult> RegisterUser([FromBody] UserRegisterDto userRegisterDto)
    {
        var result =  await authService.Register(userRegisterDto);
        return Ok(result);
    }
    
    [HttpPost("login")]
    [UserExceptions]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var result = await authService.Login(userLoginDto);
        
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
