using Gamma.RoboKP.Application.Abstractions.Services;
using Gamma.RoboKP.Application.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamma.RoboKP.Controllers;
[ApiController]
[Route("api/users")]
public class UserController(IUserService userService) : ControllerBase
{
    
    [HttpGet("role/{id}")]
    [Authorize]
    public async Task<ActionResult<string>> GetUserRole([FromRoute] long id)
    {
        var result = await userService.GetUserRole(id);
        
        if (result is null)
        {
            return NotFound($"Пользователь с id {id} не найден");
        }
        return Ok(result);
    }
    [Authorize(Roles = "admingamma")]
    [HttpGet]
    public async Task<ActionResult<List<UserToGetAll>>> GetUsers()
    {
        var users = await userService.GetAllUsers();
        return Ok(users);
    }
    
    [Authorize(Roles = "admingamma")]
    [HttpPut("setRole/{id}")]
    public async Task<ActionResult<bool>> SetRole([FromRoute] long id, [FromHeader] string role)
    {
        await userService.SetUserRole(id, role);
        return Ok();
    }
}
