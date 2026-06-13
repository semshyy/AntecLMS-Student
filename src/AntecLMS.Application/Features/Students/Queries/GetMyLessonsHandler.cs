using AntecLMS.Application.Common.Interfaces;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Repositories;
using MediatR;

namespace AntecLMS.Application.Features.Students.Queries.GetMyLessons;

public class GetMyLessonsHandler : IRequestHandler<GetMyLessonsQuery, Result<List<MyLessonItem>>>
{
  private readonly ILessonRepository _lessons;
  private readonly ICurrentUserService _currentUser;

  public GetMyLessonsHandler(ILessonRepository lessons, ICurrentUserService currentUser)
  {
    _lessons = lessons;
    _currentUser = currentUser;
  }

  public async Task<Result<List<MyLessonItem>>> Handle(GetMyLessonsQuery request, CancellationToken ct)
  {
    var lessons = await _lessons.GetByStudentUserIdAsync(_currentUser.UserId, ct);

    var data = lessons.Select(l => new MyLessonItem(
        l.Id,
        l.Title,
        l.Description,
        l.LessonDate,
        l.Group.Name,
        l.Materials.Select(m => new MyMaterialItem(
            m.Id,
            m.OriginalFileName,
            m.Description,
            m.Type.ToString().ToLower(),
            m.FilePath
        )).ToList()
    )).ToList();

    return Result<List<MyLessonItem>>.Success(data);
  }
}