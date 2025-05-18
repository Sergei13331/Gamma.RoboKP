using Gamma.RoboKP.Application.Abstractions.Services;
using Gamma.RoboKP.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gamma.RoboKP.Controllers;
[ApiController]
[Route("api/user")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult> RegisterUser(UserToRegister userToRegister)
    {
        await userService.Register(userToRegister);
        return Ok();
    }
}