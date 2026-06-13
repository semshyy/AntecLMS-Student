using AntecLMS.Application.Common.Interfaces;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Repositories;
using AntecLMS.Enums;
using MediatR;

namespace AntecLMS.Application.Features.Students.Queries.GetMyDashboard;

public class GetMyDashboardHandler : IRequestHandler<GetMyDashboardQuery, Result<MyDashboardResponse>>
{
  private readonly IStudentRepository _students;
  private readonly ILessonRepository _lessons;
  private readonly IGradeRepository _grades;
  private readonly IAttendanceRepository _attendance;
  private readonly ICurrentUserService _currentUser;

  public GetMyDashboardHandler(
      IStudentRepository students,
      ILessonRepository lessons,
      IGradeRepository grades,
      IAttendanceRepository attendance,
      ICurrentUserService currentUser)
  {
    _students = students;
    _lessons = lessons;
    _grades = grades;
    _attendance = attendance;
    _currentUser = currentUser;
  }

  public async Task<Result<MyDashboardResponse>> Handle(GetMyDashboardQuery request, CancellationToken ct)
  {
    var student = await _students.GetByUserIdAsync(_currentUser.UserId, ct);
    if (student is null)
      return Result<MyDashboardResponse>.Failure("Tələbə tapılmadı.", 404);

    // Aktiv qrup
    var activeGroup = student.GroupStudents
        .Where(gs => gs.IsActive)
        .Select(gs => gs.Group)
        .FirstOrDefault();

    var groupInfo = activeGroup is null ? null : new MyGroupInfo(
        activeGroup.Id,
        activeGroup.Name,
        activeGroup.GroupCode,
        activeGroup.Status.ToString().ToLower()
    );

    // Son 5 dərs
    var lessons = await _lessons.GetByStudentUserIdAsync(_currentUser.UserId, ct);
    var recentLessons = lessons.Take(5).Select(l => new MyRecentLesson(
        l.Id,
        l.Title,
        l.LessonDate,
        l.Materials.Count
    )).ToList();

    // Son 5 qiymət
    var grades = await _grades.GetByStudentIdAsync(student.Id, ct);
    var recentGrades = grades.Take(5).Select(g => new MyRecentGrade(
        g.Id,
        g.Lesson.Title,
        g.Score,
        g.MaxScore
    )).ToList();

    // Davamiyyət xülasəsi
    var attendances = await _attendance.GetByStudentIdAsync(student.Id, ct);
 
    var summary = new MyAttendanceSummary(
    Total: attendances.Count,
    Present: attendances.Count(a => a.Status == AttendanceStatus.Present),
    Absent: attendances.Count(a => a.Status == AttendanceStatus.ExcusedAbsence ||
                                   a.Status == AttendanceStatus.UnexcusedAbsence),
    Late: attendances.Count(a => a.Status == AttendanceStatus.Late)
);

    return Result<MyDashboardResponse>.Success(
        new MyDashboardResponse(groupInfo, recentLessons, recentGrades, summary)
    );
  }
}