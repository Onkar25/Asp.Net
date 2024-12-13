using System.Security.Claims;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository repository, IMapper mapper) : BaseApiContoller
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembersDto>>> GetUsers()
    {
        var users = await repository.GetMembersAsync();
        return Ok(users);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<MembersDto>> GetUsers(string username)
    {
        var user = await repository.GetMembersAsync(username);
        if (user == null)
        {
            return NotFound();
        }
        return user;

    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto member)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (username == null) return BadRequest("No username found in token");

        var user = await repository.GetUserByUsernameAsync(username);
        if (user == null) return BadRequest("Could not found user");

        mapper.Map(member, user);

        if (await repository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update the user");
    }
}
