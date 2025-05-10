using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers;

// [Authorize]
public class UsersController(DataContext context) : BaseApiController
{

  [AllowAnonymous] // Anyone can access this API
  [HttpGet]
  public async Task<ActionResult<IEnumerable<AppUsers>>> GetUsers()
  {
    var users = await context.Users.ToListAsync();

    return Ok(users);
  }

  [Authorize]
  [HttpGet("{id:int}")]
  public async Task<ActionResult<AppUsers>> GetUser(int id)
  {
    var user = await context.Users.FindAsync(id);

    if (user == null)
      return NotFound();

    return user;
  }
}
