using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Groups.Queries.GetGroups;

public record GetGroupsQuery(
  int? CourseId,
  int? TeacherId,
  string? Status,
  int Page = 1,
  int PerPage = 20
) : IRequest<Result<PagedResult<GroupListItem>>>;

public record GroupListItem(
  int Id,
  string Name,
  CourseRef Course,
  TeacherRef Teacher,
  int StudentsCount,
  DateOnly StartDate,
  DateOnly? EndDate,
  string Status
);

public record CourseRef(int Id, string Name);

public record TeacherRef(int Id, string Name, string Surname);
