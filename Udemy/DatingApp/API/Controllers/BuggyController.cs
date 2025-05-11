using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController(DataContext context) : BaseApiController
{
  [Authorize]
  [HttpGet("auth")]
  public ActionResult<string> GetAuth()
  {
    return "secret text";
  }

  [HttpGet("not-found")]
  public ActionResult<AppUsers> GetNotFound()
  {
    var things = context.Users.Find(-1);

    if (things == null) return NotFound();
    return things;
  }

  [HttpGet("server-error")]
  public ActionResult<AppUsers> GetServerError()
  {
    // try
    // {
    var things = context.Users.Find(-1) ?? throw new Exception("A bad thing happen");
    return things;
    // }
    // catch (Exception ex)
    // {
    //   return StatusCode(500, "Compute say no !!! means NO");
    // }
  }

  [HttpGet("bad-request")]
  public ActionResult<string> GetBadRequest()
  {
    return BadRequest("This is not good request");
  }
}
