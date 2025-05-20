using Gamma.RoboKP.Application.Abstractions.Auth;
using Gamma.RoboKP.Application.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamma.RoboKP.Controllers;
[ApiController]
[Route("api/user")]
public class UserController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(UserRegisterDto userRegisterDto)
    {
        var result =  await authService.Register(userRegisterDto);
        return Ok(result);
    }
    [HttpPost("login")]
    public async Task<ActionResult> Login(UserLoginDto userLoginDto)
    {
        return Ok();
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult> Get()
    {
        return Ok();
    }
}
