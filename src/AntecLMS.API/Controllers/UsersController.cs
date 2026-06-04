using AntecLMS.Application.Features.Users.Commands.CreateUser;
using AntecLMS.Application.Features.Users.Commands.DeleteUser;
using AntecLMS.Application.Features.Users.Commands.UpdateUser;
using AntecLMS.Application.Features.Users.Queries.GetUserById;
using AntecLMS.Application.Features.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AntecLMS.API.Controllers;

[Authorize(Roles = "Admin")]
public class UsersController : BaseApiController
{
  [HttpGet]
  public async Task<IActionResult> GetAll(
    [FromQuery] string? role,
    [FromQuery] string? status,
    [FromQuery] string? search,
    [FromQuery] int page = 1,
    [FromQuery] int perPage = 20,
    CancellationToken ct = default
  )
  {
    var result = await Mediator.Send(new GetUsersQuery(role, status, search, page, perPage), ct);
    return ToResponse(result);
  }

  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetById(int id, CancellationToken ct)
  {
    var result = await Mediator.Send(new GetUserByIdQuery(id), ct);
    return ToResponse(result);
  }

  [HttpPost]
  public async Task<IActionResult> Create(
    [FromBody] CreateUserCommand command,
    CancellationToken ct
  )
  {
    var result = await Mediator.Send(command, ct);
    return ToResponse(result);
  }

  [HttpPut("{id:int}")]
  public async Task<IActionResult> Update(
    int id,
    [FromBody] UpdateUserRequest request,
    CancellationToken ct
  )
  {
    var result = await Mediator.Send(
      new UpdateUserCommand(id, request.Name, request.Surname, request.Phone, request.Status),
      ct
    );

    if (!result.IsSuccess)
      return ToResponse(result);
    return Ok(new { message = "İstifadəçi uğurla yeniləndi.", data = result.Data });
  }

  [HttpDelete("{id:int}")]
  public async Task<IActionResult> Delete(int id, CancellationToken ct)
  {
    var result = await Mediator.Send(new DeleteUserCommand(id), ct);
    if (!result.IsSuccess)
      return ToResponse(result);
    return Ok(new { message = "İstifadəçi uğurla silindi." });
  }
}

public record UpdateUserRequest(string Name, string Surname, string? Phone, string Status);
