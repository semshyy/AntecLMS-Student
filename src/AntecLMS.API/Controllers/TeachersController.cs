using AntecLMS.Application.Features.Teachers.Commands.CreateTeacher;
using AntecLMS.Application.Features.Teachers.Commands.DeleteTeacher;
using AntecLMS.Application.Features.Teachers.Commands.UpdateTeacher;
using AntecLMS.Application.Features.Teachers.Queries.GetTeacherById;
using AntecLMS.Application.Features.Teachers.Queries.GetTeachers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AntecLMS.API.Controllers;

[Authorize(Roles = "Admin")]
public class TeachersController : BaseApiController
{
  [HttpGet]
  public async Task<IActionResult> GetAll(
    [FromQuery] int page = 1,
    [FromQuery] int perPage = 20,
    CancellationToken ct = default
  )
  {
    var result = await Mediator.Send(new GetTeachersQuery(page, perPage), ct);
    return ToResponse(result);
  }

  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetById(int id, CancellationToken ct)
  {
    var result = await Mediator.Send(new GetTeacherByIdQuery(id), ct);
    return ToResponse(result);
  }

  [HttpPost]
  public async Task<IActionResult> Create(
    [FromBody] CreateTeacherCommand command,
    CancellationToken ct
  )
  {
    var result = await Mediator.Send(command, ct);
    if (!result.IsSuccess)
      return ToResponse(result);
    return StatusCode(201, new { message = "Müəllim uğurla yaradıldı.", data = result.Data });
  }

  [HttpPut("{id:int}")]
  public async Task<IActionResult> Update(
    int id,
    [FromBody] UpdateTeacherRequest request,
    CancellationToken ct
  )
  {
    var result = await Mediator.Send(
      new UpdateTeacherCommand(id, request.Phone, request.Specialization, request.Status),
      ct
    );
    if (!result.IsSuccess)
      return ToResponse(result);
    return Ok(new { message = "Müəllim məlumatları uğurla yeniləndi.", data = result.Data });
  }

  [HttpDelete("{id:int}")]
  public async Task<IActionResult> Delete(int id, CancellationToken ct)
  {
    var result = await Mediator.Send(new DeleteTeacherCommand(id), ct);
    if (!result.IsSuccess)
      return ToResponse(result);
    return Ok(new { message = "Müəllim uğurla silindi." });
  }
}

public record UpdateTeacherRequest(string? Phone, string? Specialization, string Status);
