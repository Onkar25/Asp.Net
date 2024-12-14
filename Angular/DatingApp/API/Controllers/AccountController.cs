using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenServices token, IMapper mapper) : BaseApiContoller
{

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto login)
    {
        var user = await context.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.UserName.ToLower() == login.Username.ToLower());
        if (user == null)
        {
            return Unauthorized("Username is invalid");
        }

        using var hashmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hashmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
            {
                return Unauthorized("Password is invalid");
            }
        }
        return new UserDto
        {
            Username = user.UserName,
            Token = token.CreateToken(user),
            KnownAs = user.KnownAs,
            Gender = user.Gender,
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegistrationDto registerUser)
    {
        if (await UserExist(registerUser.Username))
        {
            return BadRequest("User already exist");
        }

        // Hashing of password
        using var hash = new HMACSHA512();
        var user = mapper.Map<AppUser>(registerUser);
        user.UserName = registerUser.Username.ToLower();
        user.PasswordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(registerUser.Password));
        user.PasswordSalt = hash.Key;

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new UserDto
        {
            Username = user.UserName,
            Gender = user.Gender,
            KnownAs = user.KnownAs,
            Token = token.CreateToken(user)
        };
    }

    private async Task<bool> UserExist(string username)
    {
        return await context.Users.AnyAsync(u => u.UserName.ToLower() == username.ToLower());
    }
}
