using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository repository, IMapper mapper, IPhotoService photoService) : BaseApiContoller
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MembersDto>>> GetUsers([FromQuery] UserParams userParams)
    {
        userParams.CurrentUsername = User.GetUsername();
        var users = await repository.GetMembersAsync(userParams);
        Response.AddPaginationHeader(users);
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
        var user = await repository.GetUserByUsernameAsync(User.GetUsername());
        if (user == null) return BadRequest("Could not found user");

        mapper.Map(member, user);

        if (await repository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update the user");
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        var user = await repository.GetUserByUsernameAsync(User.GetUsername());
        if (user == null) return BadRequest("Could not found user");

        var result = await photoService.AddPhotoAsync(file);
        if (result.Error != null) return BadRequest(result.Error.Message);

        var photo = new Photo
        {
            PublicId = result.PublicId,
            Url = result.SecureUrl.AbsoluteUri
        };
        if (user.Photos.Count == 0)
            photo.IsMain = true;

        user.Photos.Add(photo);

        if (await repository.SaveAllAsync())
        {
            // return mapper.Map<PhotoDto>(photo);
            return CreatedAtAction(nameof(GetUsers), new { username = user.UserName }, mapper.Map<PhotoDto>(photo));
        }
        return BadRequest("Problem in adding photo");

    }

    [HttpPut("set-main-photo/{photoId:int}")]
    public async Task<ActionResult> SetMainPhoto(int photoId)
    {
        var user = await repository.GetUserByUsernameAsync(User.GetUsername());
        if (user == null) return BadRequest("Could not found user");

        var photo = user.Photos.FirstOrDefault(f => f.Id == photoId);
        if (photo == null || photo.IsMain) return BadRequest("Cannot use this photo as main");

        var currentMain = user.Photos.FirstOrDefault(f => f.IsMain);
        if (currentMain != null)
            currentMain.IsMain = false;
        photo.IsMain = true;

        if (await repository.SaveAllAsync()) return NoContent();
        return BadRequest("Problem setting main photo");
    }

    [HttpDelete("delete-photo/{photoId:int}")]
    public async Task<ActionResult> DeletePhoto(int photoId)
    {
        var user = await repository.GetUserByUsernameAsync(User.GetUsername());
        if (user == null) return BadRequest("Could not found user");

        var photo = user.Photos.FirstOrDefault(f => f.Id == photoId);
        if (photo == null || photo.IsMain) return BadRequest("This photo cannot be deleted");

        if (photo.PublicId != null)
        {
            var result = await photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null)
                return BadRequest(result.Error.Message);
        }

        user.Photos.Remove(photo);

        if (await repository.SaveAllAsync()) return NoContent();
        return BadRequest("Problem while deleting photo");

    }
}
