using AntecLMS.Application.Features.Groups.Commands.AddStudentToGroup;
using AntecLMS.Application.Features.Groups.Commands.CreateGroup;
using AntecLMS.Application.Features.Groups.Commands.DeleteGroup;
using AntecLMS.Application.Features.Groups.Commands.RemoveStudentFromGroup;
using AntecLMS.Application.Features.Groups.Commands.UpdateGroup;
using AntecLMS.Application.Features.Groups.Queries.GetGroupById;
using AntecLMS.Application.Features.Groups.Queries.GetGroups;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AntecLMS.API.Controllers;

[Authorize]
public class GroupsController : BaseApiController
{
  [HttpGet]
  public async Task<IActionResult> GetAll(
    [FromQuery] int? courseId,
    [FromQuery] int? teacherId,
    [FromQuery] string? status,
    [FromQuery] int page = 1,
    [FromQuery] int perPage = 20,
    CancellationToken ct = default
  )
  {
    var result = await Mediator.Send(
      new GetGroupsQuery(courseId, teacherId, status, page, perPage),
      ct
    );
    return ToResponse(result);
  }

  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetById(int id, CancellationToken ct)
  {
    var result = await Mediator.Send(new GetGroupByIdQuery(id), ct);
    return ToResponse(result);
  }

  [HttpPost]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> Create(
    [FromBody] CreateGroupCommand command,
    CancellationToken ct
  )
  {
    var result = await Mediator.Send(command, ct);
    if (!result.IsSuccess)
      return ToResponse(result);
    return StatusCode(201, new { message = "Qrup uğurla yaradıldı.", data = result.Data });
  }

  [HttpPut("{id:int}")]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> Update(
    int id,
    [FromBody] UpdateGroupRequest request,
    CancellationToken ct
  )
  {
    var result = await Mediator.Send(
      new UpdateGroupCommand(id, request.Name, request.TeacherId, request.Status),
      ct
    );
    if (!result.IsSuccess)
      return ToResponse(result);
    return Ok(new { message = "Qrup uğurla yeniləndi.", data = result.Data });
  }

  [HttpDelete("{id:int}")]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> Delete(int id, CancellationToken ct)
  {
    var result = await Mediator.Send(new DeleteGroupCommand(id), ct);
    if (!result.IsSuccess)
      return ToResponse(result);
    return Ok(new { message = "Qrup uğurla silindi." });
  }

  [HttpPost("{id:int}/students")]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> AddStudent(
    int id,
    [FromBody] AddStudentRequest request,
    CancellationToken ct
  )
  {
    var result = await Mediator.Send(new AddStudentToGroupCommand(id, request.StudentId), ct);
    if (!result.IsSuccess)
      return ToResponse(result);
    return StatusCode(
      201,
      new { message = "Tələbə qrupa uğurla əlavə edildi.", data = result.Data }
    );
  }

  [HttpDelete("{id:int}/students/{studentId:int}")]
  [Authorize(Roles = "Admin")]
  public async Task<IActionResult> RemoveStudent(int id, int studentId, CancellationToken ct)
  {
    var result = await Mediator.Send(new RemoveStudentFromGroupCommand(id, studentId), ct);
    if (!result.IsSuccess)
      return ToResponse(result);
    return Ok(new { message = "Tələbə qrupdan uğurla çıxarıldı." });
  }
}

public record UpdateGroupRequest(string Name, int TeacherId, string Status);

public record AddStudentRequest(int StudentId);
