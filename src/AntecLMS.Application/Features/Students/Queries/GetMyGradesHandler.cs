using AntecLMS.Application.Common.Interfaces;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Repositories;
using MediatR;

namespace AntecLMS.Application.Features.Students.Queries.GetMyGrades;

public class GetMyGradesHandler : IRequestHandler<GetMyGradesQuery, Result<List<MyGradeItem>>>
{
  private readonly IGradeRepository _grades;
  private readonly IStudentRepository _students;
  private readonly ICurrentUserService _currentUser;

  public GetMyGradesHandler(
      IGradeRepository grades,
      IStudentRepository students,
      ICurrentUserService currentUser)
  {
    _grades = grades;
    _students = students;
    _currentUser = currentUser;
  }

  public async Task<Result<List<MyGradeItem>>> Handle(GetMyGradesQuery request, CancellationToken ct)
  {
    var student = await _students.GetByUserIdAsync(_currentUser.UserId, ct);
    if (student is null)
      return Result<List<MyGradeItem>>.Failure("Tələbə tapılmadı.", 404);

    var items = await _grades.GetByStudentIdAsync(student.Id, ct);

    var data = items.Select(g => new MyGradeItem(
        g.Id,
        g.Lesson.Title,
        g.Lesson.LessonDate,
        g.Score,
        g.MaxScore,
        g.Comment
    )).ToList();

    return Result<List<MyGradeItem>>.Success(data);
  }
}