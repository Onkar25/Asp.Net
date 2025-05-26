using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(UserManager<AppUsers> userManager,
ITokenService tokenService, IMapper mapper) : BaseApiController
{
  [HttpPost("register")]
  public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
  {
    if (await UserExists(registerDto.Username))
      return BadRequest("Username already exist");

    // using var hmac = new HMACSHA512();

    var user = mapper.Map<AppUsers>(registerDto);

    user.UserName = registerDto.Username.ToLower();
    // user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
    // user.PasswordSalt = hmac.Key;
    // context.Users.Add(user);
    // await context.SaveChangesAsync();

    var result = await userManager.CreateAsync(user, registerDto.Password);
    if (!result.Succeeded)
      return BadRequest(result.Errors);

    return new UserDto
    {
      Username = user.UserName,
      Token = await tokenService.CreateToken(user),
      KnownAs = user.KnownAs,
      Gender = user.Gender,
    };
  }


  [HttpPost("login")]
  public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
  {
    var user = await userManager.Users
      .Include(p => p.Photos)
      .FirstOrDefaultAsync(x =>
        x.NormalizedUserName == loginDto.Username.ToUpper());

    if (user == null || user.UserName == null)
      return Unauthorized("Invalide Username");


    var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
    if (!result)
      return Unauthorized();

    // using var hmac = new HMACSHA512(user.PasswordSalt);
    // var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
    // for (int i = 0; i < computedHash.Length; i++)
    // {
    //   if (computedHash[i] != user.PasswordHash[i])
    //     return Unauthorized("Invalid Password");
    // }

    return new UserDto
    {
      Username = user.UserName,
      KnownAs = user.KnownAs,
      Token = await tokenService.CreateToken(user),
      Gender = user.Gender,
      PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
    };
  }
  private async Task<bool> UserExists(string username)
  {
    return await userManager.Users.AnyAsync(x => x.NormalizedUserName == username.ToUpper());
  }
}
