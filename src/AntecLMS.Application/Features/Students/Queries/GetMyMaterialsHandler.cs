using AntecLMS.Application.Common.Interfaces;
using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Repositories;
using MediatR;

namespace AntecLMS.Application.Features.Students.Queries.GetMyMaterials;

public class GetMyMaterialsHandler : IRequestHandler<GetMyMaterialsQuery, Result<List<MyMaterialDetail>>>
{
  private readonly IMaterialRepository _materials;
  private readonly ICurrentUserService _currentUser;

  public GetMyMaterialsHandler(IMaterialRepository materials, ICurrentUserService currentUser)
  {
    _materials = materials;
    _currentUser = currentUser;
  }

  public async Task<Result<List<MyMaterialDetail>>> Handle(GetMyMaterialsQuery request, CancellationToken ct)
  {
    var items = await _materials.GetByStudentUserIdAsync(_currentUser.UserId, ct);

    var data = items.Select(m => new MyMaterialDetail(
        m.Id,
        m.OriginalFileName,
        m.Description,
        m.Type.ToString().ToLower(),
        m.FilePath,
        m.Lesson.Title,
        m.Lesson.LessonDate
    )).ToList();

    return Result<List<MyMaterialDetail>>.Success(data);
  }
}