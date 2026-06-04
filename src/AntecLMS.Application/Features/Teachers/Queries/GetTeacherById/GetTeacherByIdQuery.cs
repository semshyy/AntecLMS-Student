using AntecLMS.Application.Common.Models;
using MediatR;

namespace AntecLMS.Application.Features.Teachers.Queries.GetTeacherById;

public record GetTeacherByIdQuery(int Id) : IRequest<Result<TeacherDetailResponse>>;

public record TeacherDetailResponse(
  int Id,
  int UserId,
  string Name,
  string Surname,
  string Email,
  string? Phone,
  string? Specialization,
  string? Bio,
  string Status,
  List<GroupRef> Groups
);

public record GroupRef(int Id, string Name);
