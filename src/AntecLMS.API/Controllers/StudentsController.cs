using AntecLMS.Application.Features.Students.Commands.ChangePassword;
using AntecLMS.Application.Features.Students.Queries.GetMyAttendance;
using AntecLMS.Application.Features.Students.Queries.GetMyDashboard;
using AntecLMS.Application.Features.Students.Queries.GetMyGrades;
using AntecLMS.Application.Features.Students.Queries.GetMyLessons;
using AntecLMS.Application.Features.Students.Queries.GetMyMaterials;
using AntecLMS.Application.Features.Students.Queries.GetMyProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AntecLMS.API.Controllers;

[Authorize(Roles = "Student")]
[Route("api/me")]
public class StudentPortalController : BaseApiController
{
  [HttpGet("dashboard")]
  public async Task<IActionResult> GetMyDashboard(CancellationToken ct)
  {
    var result = await Mediator.Send(new GetMyDashboardQuery(), ct);
    return ToResponse(result);
  }

  [HttpGet("lessons")]
  public async Task<IActionResult> GetMyLessons(CancellationToken ct)
  {
    var result = await Mediator.Send(new GetMyLessonsQuery(), ct);
    return ToResponse(result);
  }

  [HttpGet("attendance")]
  public async Task<IActionResult> GetMyAttendance(CancellationToken ct)
  {
    var result = await Mediator.Send(new GetMyAttendanceQuery(), ct);
    return ToResponse(result);
  }

  [HttpGet("grades")]
  public async Task<IActionResult> GetMyGrades(CancellationToken ct)
  {
    var result = await Mediator.Send(new GetMyGradesQuery(), ct);
    return ToResponse(result);
  }

  [HttpGet("materials")]
  public async Task<IActionResult> GetMyMaterials(CancellationToken ct)
  {
    var result = await Mediator.Send(new GetMyMaterialsQuery(), ct);
    return ToResponse(result);
  }

  [HttpPut("change-password")]
  public async Task<IActionResult> ChangePassword(
      [FromBody] ChangePasswordCommand command,
      CancellationToken ct)
  {
    var result = await Mediator.Send(command, ct);
    return ToResponse(result);
  }
  [HttpGet("profile")]
  public async Task<IActionResult> GetMyProfile(CancellationToken ct)
  {
    var result = await Mediator.Send(new GetMyProfileQuery(), ct);
    return ToResponse(result);
  }
}