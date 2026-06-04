using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Students.Queries.GetStudents;

public record GetStudentsQuery(
  int? GroupId,
  string? Status,
  string? Search,
  int Page = 1,
  int PerPage = 20
) : IRequest<Result<PagedResult<StudentListItem>>>;

public record StudentListItem(
  int Id,
  int UserId,
  string Name,
  string Surname,
  string Email,
  string? Phone,
  DateOnly? BirthDate,
  string Status,
  List<GroupRefS> Groups
);

public record GroupRefS(int Id, string Name);
