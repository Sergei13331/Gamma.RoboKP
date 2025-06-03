using System.Security.Claims;
using Gamma.RoboKP.Domain.Abstractions.Services;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Domain.Models;
using Gamma.RoboKP.Filters.ExceptionsFilters;
using Gamma.RoboKP.Models.User;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamma.RoboKP.Controllers;
[ApiController]
[Route("api/users")]
public class UserController(IUserService userService, IMapper mapper) : ControllerBase
{
    
    [HttpGet("{id}/role")]
    [Authorize(Roles = RoleConsts.AdminGamma)]
    [AuthExceptions]
    public async Task<ActionResult<string>> GetUserRole([FromRoute] long id)
    {
        var result = await userService.GetUserRole(id);
        
        if (result is null)
        {
            return NotFound($"Пользователь с id {id} не найден");
        }
        return Ok(result);
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<UserToGet>> GetUserByEmail([FromRoute] string email)
    {
        var user = await userService.GetUserByEmail(email);
        
        if (user is null) return NotFound();
        
        var response = mapper.Map<User, UserToGet>(user);
        
        return Ok(response);
    }

    [Authorize(Roles = RoleConsts.AdminGamma)]
    [HttpGet("{id}")]
    [AuthExceptions]
    public async Task<ActionResult<UserToGet>> GetUser([FromRoute] long id)
    {
        var user = await userService.GetUserById(id);
        if (user is null) return NotFound();
        
        var response = mapper.Map<User, UserToGet>(user);
        
        return Ok(response);
    }
    
    [Authorize(Roles = RoleConsts.AdminGamma)]
    [HttpGet]
    public async Task<ActionResult<List<UserToGet>>> GetUsers()
    {
        var users = await userService.GetAllUsers();
        
        var response = mapper.Map<List<User>, List<UserToGet>>(users);
        
        return Ok(response);
    }
    
    [Authorize(Roles = RoleConsts.AdminGamma)] //TODO: admingamma как сonst
    [HttpPatch("{id}/role")]
    [AuthExceptions]
    public async Task<ActionResult<bool>> SetRole([FromRoute] long id, [FromHeader] string role)
    {
        await userService.SetUserRole(id, role);
        return Ok();
    }
    
    [Authorize(Roles = RoleConsts.AdminGamma)]
    [HttpGet("{id}/status")]
    [AuthExceptions]
    public async Task<ActionResult<string>> GetStatus([FromRoute] long id)
    {
        var result = await userService.GetUserStatus(id);
        return Ok(result);
    }

    [Authorize(Roles = RoleConsts.AdminGamma)]
    [HttpPatch("{id}/setStatus")]
    [AuthExceptions]
    public async Task<ActionResult<bool>> SetStatus([FromRoute] long id, [FromHeader] string status)
    {
        await userService.SetStatus(id, status);
        return Ok();
    }
    
    [Authorize]
    [HttpPatch("me")]
    public async Task<ActionResult> Update([FromBody] UserToUpdate userToUpdate)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null) return Unauthorized();
        
        var longUserId = long.Parse(userId);
        
        var response = await userService.UpdateUser(longUserId, userToUpdate.FirstName, userToUpdate.SurName, userToUpdate.LastName, userToUpdate.Email);
        
        if (response) return Ok(); 
        return BadRequest(response);
    }

    [Authorize(Roles = RoleConsts.AdminGamma)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete([FromRoute] long id)
    {
        var response = await userService.DeleteUser(id);
        if (response) return Ok();
        
        return NotFound();
    }
}
