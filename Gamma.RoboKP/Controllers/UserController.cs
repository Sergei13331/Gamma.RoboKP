using System.Security.Claims;
using Gamma.RoboKP.Application.Abstractions.Services;
using Gamma.RoboKP.Application.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamma.RoboKP.Controllers;
[ApiController]
[Route("api/users")]
public class UserController(IUserService userService) : ControllerBase
{
    
    [HttpGet("{id}/role")]
    [Authorize(Roles = "admingamma")]
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
    [HttpGet("{id}")]
    public async Task<ActionResult<UserToGet>> GetUser([FromRoute] long id)
    {
        var response = await userService.GetUserById(id);
        return Ok(response);
    }
    
    [Authorize(Roles = "admingamma")]
    [HttpGet]
    public async Task<ActionResult<List<UserToGet>>> GetUsers()
    {
        var users = await userService.GetAllUsers();
        return Ok(users);
    }
    
    [Authorize(Roles = "admingamma")]// как сonst
    [HttpPatch("{id}/role")]
    public async Task<ActionResult<bool>> SetRole([FromRoute] long id, [FromHeader] string role)
    {
        await userService.SetUserRole(id, role);
        return Ok();
    }
    
    [Authorize(Roles = "admingamma")]
    [HttpGet("{id}/status")]
    public async Task<ActionResult<string>> GetStatus([FromRoute] long id)
    {
        var result = await userService.GetUserStatus(id);
        return Ok(result);
    }

    [Authorize(Roles = "admingamma")]
    [HttpPatch("{id}/setStatus")]
    public async Task<ActionResult<bool>> SetStatus([FromRoute] long id, [FromHeader] string status)
    {
        await userService.SetStatus(id, status);
        return Ok();
    }
    
    [Authorize]
    [HttpPatch("me")]
    public async Task<ActionResult<bool>> Update([FromBody] UserToUpdate userToUpdate)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null) return Unauthorized();
        
        var longUserId = long.Parse(userId);
        
        var response = await userService.UpdateUser(longUserId, userToUpdate);
        
        if (response) return Ok(); 
        return BadRequest(response);
    }

    [Authorize(Roles = "admingamma")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete([FromRoute] long id)
    {
        var response = await userService.DeleteUser(id);
        if (response) return Ok();
        
        return NotFound();
    }
}
