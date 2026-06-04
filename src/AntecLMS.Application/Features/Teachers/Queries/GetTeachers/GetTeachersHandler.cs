using AntecLMS.Application.Common.Models;
using AntecLMS.Domain.Repositories;
using MediatR;

namespace AntecLMS.Application.Features.Teachers.Queries.GetTeachers;

public class GetTeachersHandler
  : IRequestHandler<GetTeachersQuery, Result<PagedResult<TeacherListItem>>>
{
  private readonly ITeacherRepository _teachers;

  public GetTeachersHandler(ITeacherRepository teachers) => _teachers = teachers;

  public async Task<Result<PagedResult<TeacherListItem>>> Handle(
    GetTeachersQuery request,
    CancellationToken ct
  )
  {
    var (items, total) = await _teachers.GetPagedAsync(request.Page, request.PerPage, ct);

    var data = items
      .Select(t => new TeacherListItem(
        t.Id,
        t.UserId,
        t.User.Name,
        t.User.Surname,
        t.User.Email,
        t.User.Phone,
        t.Specialization,
        t.Status.ToString().ToLower()
      ))
      .ToList();

    return Result<PagedResult<TeacherListItem>>.Success(
      new PagedResult<TeacherListItem>
      {
        Data = data,
        Total = total,
        Page = request.Page,
        PerPage = request.PerPage,
      }
    );
  }
}
