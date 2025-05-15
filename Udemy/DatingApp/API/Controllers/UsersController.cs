using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
{
  // Anyone can access this API
  [HttpGet]
  public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
  {
    var users = await userRepository.GetMemberAsyn();
    return Ok(users);
  }

  [HttpGet("{username}")]
  public async Task<ActionResult<MemberDto>> GetUser(string username)
  {
    var user = await userRepository.GetMemberAsync(username);

    if (user == null)
      return NotFound();

    return user;
  }

  [HttpPut]
  public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
  {
    var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (username == null)
      return BadRequest("No username found");

    var user = await userRepository.GetUserByUsernameAsync(username);

    if (user == null)
      return BadRequest("No user found");

    mapper.Map(memberUpdateDto, user);
    if (await userRepository.SaveAllAsync())

      return NoContent();

    return BadRequest("Failed to update the user");
  }
}
