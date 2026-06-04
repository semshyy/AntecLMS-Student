using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Teachers.Queries.GetTeachers;

public record GetTeachersQuery(int Page = 1, int PerPage = 20)
  : IRequest<Result<PagedResult<TeacherListItem>>>;

public record TeacherListItem(
  int Id,
  int UserId,
  string Name,
  string Surname,
  string Email,
  string? Phone,
  string? Specialization,
  string Status
);
