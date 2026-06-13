using AntecLMS.Application.Common.Interfaces;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Repositories;
using MediatR;

namespace AntecLMS.Application.Features.Students.Queries.GetMyAttendance;

public class GetMyAttendanceHandler : IRequestHandler<GetMyAttendanceQuery, Result<List<MyAttendanceItem>>>
{
  private readonly IAttendanceRepository _attendance;
  private readonly IStudentRepository _students;
  private readonly ICurrentUserService _currentUser;

  public GetMyAttendanceHandler(
      IAttendanceRepository attendance,
      IStudentRepository students,
      ICurrentUserService currentUser)
  {
    _attendance = attendance;
    _students = students;
    _currentUser = currentUser;
  }

  public async Task<Result<List<MyAttendanceItem>>> Handle(GetMyAttendanceQuery request, CancellationToken ct)
  {
    var student = await _students.GetByUserIdAsync(_currentUser.UserId, ct);
    if (student is null)
      return Result<List<MyAttendanceItem>>.Failure("Tələbə tapılmadı.", 404);

    var items = await _attendance.GetByStudentIdAsync(student.Id, ct);

    var data = items.Select(a => new MyAttendanceItem(
        a.Id,
        a.Lesson.LessonDate,
        a.Lesson.Title,
        a.Status.ToString().ToLower(),
        a.LateMinutes,
        a.Reason
    )).ToList();

    return Result<List<MyAttendanceItem>>.Success(data);
  }
}