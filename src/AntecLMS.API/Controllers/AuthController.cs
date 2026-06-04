using AntecLMS.Application.Features.Auth.Commands.Login;
using AntecLMS.Application.Features.Auth.Commands.Logout;
using AntecLMS.Application.Features.Auth.Queries.GetMe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AntecLMS.API.Controllers;

public class AuthController : BaseApiController
{
  [HttpPost("login")]
  [AllowAnonymous]
  public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken ct)
  {
    var result = await Mediator.Send(command, ct);

    if (!result.IsSuccess)
      return Unauthorized(new { message = result.Error });

    return Ok(new { token = result.Data!.Token, user = result.Data.User });
  }

  [HttpGet("me")]
  [Authorize]
  public async Task<IActionResult> Me(CancellationToken ct)
  {
    var result = await Mediator.Send(new GetMeQuery(), ct);
    return ToResponse(result);
  }

  [HttpPost("logout")]
  [Authorize]
  public async Task<IActionResult> Logout(CancellationToken ct)
  {
    var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    var result = await Mediator.Send(new LogoutCommand(token), ct);
    return ToResponse(result);
  }
}
