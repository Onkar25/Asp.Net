using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository repository) : BaseApiContoller
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
}
