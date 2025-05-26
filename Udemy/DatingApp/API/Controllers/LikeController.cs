using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class LikesController(IUnitOfWork unitOfWork) : BaseApiController
{
  [HttpPost("{targetUserId:int}")]
  public async Task<ActionResult> ToggleLike(int targetUserId)
  {
    var sourceUserId = User.GetUserId();

    if (sourceUserId == targetUserId)
      return BadRequest("You cannot like yourselft");

    var existingLike = await unitOfWork.LikeRepository.GetUserLike(sourceUserId, targetUserId);

    if (existingLike == null)
    {
      var like = new UserLike
      {
        SourceUserId = sourceUserId,
        TargetUserId = targetUserId
      };
      unitOfWork.LikeRepository.AddLike(like);
    }
    else
    {
      unitOfWork.LikeRepository.DeleteLike(existingLike);
    }

    if (await unitOfWork.Complete())
      return Ok();

    return BadRequest("Failed to update like");
  }

  [HttpGet("list")]
  public async Task<ActionResult<IEnumerable<int>>> GetCurrentUserLikeIds()
  {
    return Ok(await unitOfWork.LikeRepository.GetCurrentUserLikeIds(User.GetUserId()));
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<MemberDto>>> GetUserLikes([FromQuery] LikeParams likeParams)
  {
    likeParams.UserId = User.GetUserId();
    var user = await unitOfWork.LikeRepository.GetUserLikes(likeParams);

    Response.AddPaginationHeader(user);

    return Ok(user);
  }
}
